package dev.betterendfield.android;

/** Host JVM regression tests for the production selection transaction. */
public final class BemHotSwitchUpdateTest {
    public static void main(String[] args) throws Exception {
        BemHotSwitchUpdate transaction=new BemHotSwitchUpdate("A");
        String[] index={"A"};int[] prepared={0},published={0};
        BemHotSwitchUpdate.IndexReader reader=()->index[0];
        BemHotSwitchUpdate.Preparer preparer=value->{++prepared[0];return "config:"+value;};
        BemHotSwitchUpdate.Publisher publisher=value->{++published[0];return true;};
        check(!transaction.update(reader,preparer,publisher)&&prepared[0]==0,"unchanged selection");
        index[0]="B";
        check(transaction.update(reader,preparer,publisher)&&published[0]==1,"A to B");
        check(!transaction.update(reader,preparer,publisher)&&prepared[0]==1,"duplicate selection");
        index[0]="A";
        check(!transaction.update(reader,preparer,value->false),"queue rejection");
        int preparedBeforeRetry=prepared[0];
        check(transaction.update(reader,preparer,publisher),"queue retry and B to A");
        check(prepared[0]==preparedBeforeRetry,"not-ready bridge caused repeated package preparation");
        index[0]="B";
        try {transaction.update(reader,value->{throw new java.io.IOException("copy failed");},publisher);
            throw new AssertionError("preparation failure not propagated");
        } catch(java.io.IOException expected) {}
        check(transaction.update(reader,preparer,publisher),"preparation retry");
        index[0]="C";int previousPublished=published[0];
        check(!transaction.update(reader,value->{index[0]="D";return "stale C";},publisher),"superseded preparation");
        check(published[0]==previousPublished,"stale configuration published");
        check(transaction.update(reader,preparer,publisher),"latest selection after superseded preparation");
        index[0]="[]";
        check(transaction.update(reader,preparer,publisher),"disable all packages");
        String[] shapeIndex={"{\"selected_options\":\"body:on\",\"selected_parameters\":\"shape:400\"}"};
        BemHotSwitchUpdate shapeTransaction=new BemHotSwitchUpdate(shapeIndex[0]);
        java.util.List<String> queuedShapes=new java.util.ArrayList<>();
        shapeIndex[0]="{\"selected_options\":\"body:on\",\"selected_parameters\":\"shape:450\"}";
        check(shapeTransaction.update(()->shapeIndex[0],value->value,value->{queuedShapes.add(value);return true;}),"parameter-only update");
        check(queuedShapes.size()==1 && queuedShapes.get(0).contains("shape:450") && queuedShapes.get(0).contains("body:on"),"parameter update lost merged options");
        shapeIndex[0]="{\"selected_options\":\"body:off\",\"selected_parameters\":\"shape:460\"}";
        check(!shapeTransaction.update(()->shapeIndex[0],value->{
            shapeIndex[0]="{\"selected_options\":\"body:off\",\"selected_parameters\":\"shape:470\"}";return value;
        },value->{queuedShapes.add(value);return true;}),"superseded slider apply");
        check(queuedShapes.size()==1,"superseded slider state reached native bridge");
        check(shapeTransaction.update(()->shapeIndex[0],value->value,value->{queuedShapes.add(value);return true;}) && queuedShapes.get(1).contains("shape:470"),"latest slider state after superseded apply");
        System.out.println("PASS BEM hot-switch transaction: changes, retries, superseded candidates and empty selection");
    }
    private static void check(boolean condition,String message) {
        if(!condition) throw new AssertionError(message);
    }
}
