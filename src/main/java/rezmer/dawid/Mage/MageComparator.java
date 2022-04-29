package rezmer.dawid.Mage;





import rezmer.dawid.Mage.Mage;

import java.util.Comparator;


public class MageComparator implements Comparator<Mage> {

    @Override
    public int compare(Mage m1, Mage m2) {
        int ret;

            ret = (int)m1.getPower()-(int)m2.getPower();

        if (ret == 0) {
            ret = m1.getLevel() - m2.getLevel();
        }
        if(ret==0) {
            ret = m1.getName() == null
                    ? (m2.getName() == null ? 0 : 1)
                    : m1.getName().compareTo(m2.getName());
        }
        return ret;
    }

}



