package Entities;

import javax.persistence.*;
import java.io.Serializable;
import java.util.*;

@Entity
public class Brewery  {
    private int value;
    @Id
    private String name;
    @OneToMany (mappedBy = "brewery",cascade = CascadeType.ALL,fetch = FetchType.EAGER)
    private List<Beer> beers ;

    @Override
    public String toString() {
        return "Entities.Brewery{" +
                "value=" + value +
                ", name='" + name + '\'' +
                '}';
    }

    public Brewery(){
        this.beers=new ArrayList<>();
    }

    public Brewery(int height, String name) {
        this.value = height;
        this.name = name;
        this.beers=new ArrayList<>(); }

    public int getValue() {
        return value;
    }

    public void setValue(int value) {
        this.value = value;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Beer> getBeers() {
        return beers;
    }
    public void addBeer(Beer beer){
        beers.add(beer);
    }

    public void setBeers(List<Beer> beers) {
        this.beers = beers;
    }
}
