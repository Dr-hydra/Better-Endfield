package dev.betterendfield.next;
import java.io.InputStream;
final class BemInstalledResources {
    interface Source { InputStream open(String name) throws Exception; }
}
