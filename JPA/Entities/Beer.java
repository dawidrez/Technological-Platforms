package Entities;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import java.io.Serializable;

@Entity
public class Beer implements Serializable {
    @Id
    private String name;
    private int price;
    @ManyToOne @JoinColumn(name = "brewery")
    private Brewery brewery;

    @Override
    public String toString() {
        return "Entities.Beer{" +
                "name='" + name + '\'' +
                ", price=" + price +
                ", brewery=" + brewery +
                '}';
    }

    public Beer(String name, int price) {
        this.name = name;
        this.price = price;
    }

    public Beer() {
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getPrice() {
        return price;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public Brewery getBrewery() {
        return brewery;
    }

    public void setBrewery(Brewery brewery) {
        this.brewery = brewery;
        if(brewery!=null)
            brewery.addBeer(this);
    }
}
