package rezmer.dawid;
import rezmer.dawid.Mage.Mage;

import java.util.*;


public class Application {


    public static void main(String[] args) {

        boolean sorted = Boolean.parseBoolean(args[0]);
        boolean alternativeCriteria = Boolean.parseBoolean(args[1]);
        Mage king=new Mage("Dawid", 20, 5.1);
        king.addMage(sorted,alternativeCriteria,new Mage("Fred", 2, 51));
        king.addMage(sorted,alternativeCriteria,new Mage("Paul", 23, 51));
        king.addMage(sorted,alternativeCriteria,new Mage("Agnes", 12, 5.1));
        king.addMage(sorted,alternativeCriteria,new Mage("David", 54, 12));
        Mage king2=new Mage("Leon", 20, 5.1);
        king2.addMage(sorted,alternativeCriteria,new Mage("Peter", 2, 2.3));
        king2.addMage(sorted,alternativeCriteria,new Mage("John", 0, 2.4));
        king2.addMage(sorted,alternativeCriteria,new Mage("Paul2", 30, 65));
        king.addMage(sorted,alternativeCriteria,king2);
        king.writeRecursive(1);
        Map <Mage,java.lang.Integer>mapa=king.createMap(sorted,alternativeCriteria);
        System.out.println(mapa);
    }



}