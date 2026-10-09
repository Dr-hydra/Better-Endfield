package dev.betterendfield.next;

/** Single-worker transaction: never queue a prepared but superseded selection. */
final class BemHotSwitchUpdate {
    interface IndexReader { String read() throws Exception; }
    interface Preparer { String prepare(String index) throws Exception; }
    interface Publisher { boolean queue(String configuration) throws Exception; }

    private String queuedIndex;
    private String preparedIndex,preparedConfiguration;

    BemHotSwitchUpdate(String initialIndex) { queuedIndex=initialIndex; }

    boolean update(IndexReader reader,Preparer preparer,Publisher publisher) throws Exception {
        String candidate=reader.read();
        if(candidate.equals(queuedIndex)) {
            preparedIndex=null;preparedConfiguration=null;return false;
        }
        if(!candidate.equals(preparedIndex)) {
            preparedIndex=null;preparedConfiguration=null;
            preparedConfiguration=preparer.prepare(candidate);
            preparedIndex=candidate;
        }
        if(!candidate.equals(reader.read())) {
            preparedIndex=null;preparedConfiguration=null;return false;
        }
        if(!publisher.queue(preparedConfiguration)) return false;
        queuedIndex=candidate;
        preparedIndex=null;preparedConfiguration=null;
        return true;
    }
}
