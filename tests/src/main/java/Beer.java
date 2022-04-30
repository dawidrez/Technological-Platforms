public class Beer {
    private String name;
    private int percentage;

    public Beer(String name, int percentage) {
        this.name = name;
        this.percentage = percentage;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getPertcentage() {
        return percentage;
    }

    public void setPercentage(int level) {
        this.percentage = level;
    }

    @Override
    public String toString() {
        return "Beer{" +
                "name='" + name + '\'' +
                ", percentage=" + percentage +
                '}';
    }
}
