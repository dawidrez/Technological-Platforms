package rezmer.dawid.Mage;

import java.util.*;

public class Mage  implements Comparable<Mage> {


    private String name;
    private int level;
    private double power;
    private Set<Mage> apprentices;

    public Mage(String name, int level, double power, Set<Mage> apprentices) {
        this.name = name;
        this.level = level;
        this.power = power;
        this.apprentices = apprentices;
    }

    public Mage(String name, int level, double power) {
        this.name = name;
        this.level = level;
        this.power = power;
        this.apprentices=null;
    }
    public void  writeRecursive(int num){
        for(int i=0;i<num;i++)
            System.out.print("-");
        System.out.print(this);
        System.out.print("\n");
        if(apprentices !=null && !apprentices.isEmpty()){
            for(Mage m:apprentices){
                m.writeRecursive(num+1);
            }
        }

    }


    public Map<Mage,java.lang.Integer>createMap(boolean sorted,boolean alternative){
        Map<Mage, java.lang.Integer> map;
        if (sorted) {
            if (alternative) {
                MageComparator com=new MageComparator();
                 map = new TreeMap<Mage, java.lang.Integer>(com);
            }
            else{
                map = new TreeMap<Mage, java.lang.Integer>();

            }
        }
        else
        {
            map= new HashMap<Mage, java.lang.Integer>();
        }
        if(apprentices!=null){
            for(Mage mage :apprentices){
                map.putAll(mage.createMap(sorted, alternative));
            }

        }
        map.put(this, map.size());
        return map;
    }


    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public double getPower() {
        return power;
    }

    public void setPower(double power) {
        this.power = power;
    }

    public Set<Mage> getApprentices() {
        return apprentices;
    }

    public void setApprentices(Set<Mage> apprentices) {
        this.apprentices = apprentices;
    }
 public void addMage(boolean sorted,boolean alternative,Mage m){
        if(apprentices!=null)
        {
            apprentices.add(m);
            return;
        }
        else if(sorted) {
            if(alternative){
                MageComparator com=new MageComparator();
                apprentices=new TreeSet<Mage>(com);

            }
            else{
                apprentices=new TreeSet<Mage>();
            }
        }
        else {
            apprentices=new HashSet<Mage>();
        }
        apprentices.add(m);
        return;

 }
    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Mage mage = (Mage) o;
        return level == mage.level && Double.compare(mage.power, power) == 0 && Objects.equals(name, mage.name) && Objects.equals(apprentices, mage.apprentices);
    }

    @Override
    public int hashCode() {
        int ret=0;
        for(char i:name.toCharArray())
        {
            ret+=(int)i*7;
        }
        ret+=(int)power*11;
        ret+=level*19;
        return ret;
    }

    @Override
    public String toString() {
        return "Mage{" +
                "name='" + name  +
                ", level=" + level +
                ", power=" + power +'}';
    }

    @Override
    public int compareTo(Mage other) {
        int ret = name == null
                ? (other.name == null ? 0 : 1)
                : name.compareTo(other.getName());
        if (ret == 0) {
            ret = (int)power-(int)other.getPower();
        }
        if (ret == 0) {
            ret = level - other.getLevel();
        }
        return ret;
    }

}
