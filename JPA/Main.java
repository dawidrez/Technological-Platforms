
import Entities.Beer;
import Entities.Brewery;
import Repository.BeerRepository;
import Repository.BreweryRepository;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    public static void main(String args[]) {
        EntityManagerFactory emf = Persistence.createEntityManagerFactory("rpg3Pu");
        BeerRepository mRep = new BeerRepository(emf);
        BreweryRepository tRep = new BreweryRepository(emf);
        ArrayList<Beer> beers;
        ArrayList<Brewery> brewery;
        Scanner scanner = new Scanner(System.in);
        int number;
        while (true) {
            System.out .println("Co chcesz zrobic? Wpisz odpowiednia liczbe");
            System.out .println("1- wyswietl wszystkie piwa ");
            System.out .println("2- wyswietl wszystkie browary");
            System.out .println("3- wyswietl piwa tansze niz");
            System.out .println("4- wyswietl browary tansze niz");
            System.out .println("5- wyswietl piwa z danego browaru drozsze niz");
            System.out .println("6- dodaj nowy browar");
            System.out .println("7- dodaj nowe piwo");
            System.out .println("8- usun browar");
            System.out .println("9- usun piwo, ktore nie nalezy do zadnego browaru");
            System.out .println("10- zakoncz");
            number = scanner.nextInt();
            if (number == 1) {
                beers = (ArrayList<Beer>) mRep.findAll();
                for (Beer beer : beers) {
                    System.out.println(beer.toString());
                }
            }
            else if (number == 2) {
                brewery = (ArrayList<Brewery>) tRep.findAll();
                for (Brewery brew : brewery) {
                    System.out.println(brew.toString());
                }
            }
            else if (number == 3) {
                System.out.println("Wpisz cene");
                number = scanner.nextInt();
                beers = (ArrayList<Beer>) mRep.BeerCheaperThan(number);
                if (beers.size()>0)
                    for (Beer beer : beers) {
                        System.out.println(beer.toString());
                    }
                else{
                    System.out.println("Nie znaleziono zadnych piw");
                }
            }
            else if (number == 4) {
                System.out.println("Wpisz cene");
                number = scanner.nextInt();
                brewery = (ArrayList<Brewery>) tRep.BreweryCheaperThan(number);
                if (brewery.size()>0)
                    for (Brewery brew : brewery) {
                        System.out.println(brew.toString());
                    }
                else{
                    System.out.println("Nie znaleziono zadnych browarow");
                }

            }
            else if (number == 7) {
                System.out.println("Wpisz nazwe piwa, cene i browar do jakiego ma nalezec");
                String name = scanner.next();
                number = scanner.nextInt();
                String nameBrewery = scanner.next();
                Beer piwo = new Beer(name, number);
                Brewery brewer = tRep.find(nameBrewery);
                if (brewer != null) {
                    piwo.setBrewery(brewer);
                } else {
                      piwo.setBrewery(null);
                }
                mRep.add(piwo);

            }
            else if (number == 6) {
                System.out.println("Wpisz nazwe browaru oraz wartosc");
                String name = scanner.next();
                number = scanner.nextInt();
                Brewery brewer = new Brewery(number, name);
                tRep.add(brewer);

            }
            else if (number == 5) {
                System.out.println("Wpisz nazwe browaru oraz cene");
                String name = scanner.next();
                number = scanner.nextInt();
                Brewery brewer = tRep.find(name);
                if (brewer == null)
                    System.out.println("Nie ma takiego browaru!");
                else {
                    beers = (ArrayList<Beer>) mRep.BeerExpensiveBrewery(number, brewer);
                    for (Beer beer : beers) {
                        System.out.println(beer.toString());
                    }
                }
            }
            else if(number==8) {
                System.out.println("Wpisz nazwe browaru");
                String name = scanner.next();
                Brewery brewer = tRep.find(name);
                if (brewer == null)
                    System.out.println("Nie ma takiego browaru!");
                else {
                    tRep.delete(brewer);
                }
            }
            else if(number==9) {
                System.out.println("Wpisz nazwe piwa");
                String name = scanner.next();
                Beer beer = mRep.find(name);
                if (beer == null)
                    System.out.println("Nie ma takiego piwa!");
                else {
                    mRep.delete(beer);
                }
            }

            else if (number == 10)
                break;
        }

        emf.close();
    }
}
