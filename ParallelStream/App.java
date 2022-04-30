package rezmer.dawid;
import org.apache.commons.lang3.tuple.Pair;
import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.awt.image.RenderedImage;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ForkJoinPool;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class App {

    private static BufferedImage transformImage(BufferedImage img) {
        BufferedImage ret = new BufferedImage(img.getWidth(),
                img.getHeight(),
                img.getType());
        for (int i = 0; i < img.getWidth(); i++) {
            for (int j = 0; j < img.getHeight(); j++) {
                int rgb = img.getRGB(i, j);
                Color color = new Color(rgb);
                int red = color.getRed();
                int blue = color.getBlue();
                int green = color.getGreen();
                int newRed=(int)(0.299*red);
                int newGreen=(int)(0.587* green);
                int newBlue=(int)(0.114*blue);
                Color outColor = new Color(newRed,newGreen,newBlue);
                rgb = outColor.getRGB();
                ret.setRGB(i, j, rgb);
            }
        }
        return ret;
    }

    public static void main(String args[]) {
        {

            String FolderIn = args[0];
            String FolderOut = args[1];
            //int nThreads = Integer.parseInt(args[2]);
            List<Path> files;
            Path source = Path.of(FolderIn);
            try (Stream<Path> stream = Files.list(source)) {
                files = stream.collect(Collectors.toList());
                for (int j = 1; j < 6; j += 1) {
                    long diff;
                    long sum = 0;
                    for (int i = 0; i < 4; i++) {
                        long time = System.currentTimeMillis();
                        transformFiles(j, files, FolderOut);
                        diff = System.currentTimeMillis() - time;
                        sum += diff;
                    }
                    System.out.println("Dla " + String.valueOf(j) + " watkow sredni czas wykonania to " + String.valueOf(sum / 4));
                }
            } catch (IOException e) {
            }

        }
    }


    public static void transformFiles(int nThreads, List<Path> files, String FolderOut) {
        ForkJoinPool pool = new ForkJoinPool(nThreads);
        try {
            pool.submit(() -> {
                Stream<Path> stream1 = files.stream().parallel();
                Stream<Pair> pairStream = stream1.map(value -> {
                    try {
                        BufferedImage image = ImageIO.read(value.toFile());
                        String name = String.valueOf(value.getFileName());
                        Pair<String, BufferedImage> pair = Pair.of(name, image);
                        return Pair.of(name, image);
                    } catch (IOException e) {
                        System.out.println("wyjatek 1");
                        return null;
                    }
                });
                Stream<Pair> pair2Stream = pairStream.map(value -> {
                    BufferedImage image = transformImage((BufferedImage) value.getRight());
                    Pair<String, BufferedImage> ret = Pair.of((String) value.getLeft(), image);
                    return ret;
                });
                pair2Stream.forEach(value -> {
                    String dest = FolderOut + "\\" + (String) value.getLeft();
                    RenderedImage img = (RenderedImage) value.getRight();
                    Path destination = Path.of(dest);
                    try {
                        ImageIO.write((BufferedImage) value.getRight(), "jpg", destination.toFile());

                    } catch (IOException e) {
                        System.out.println(e);
                    }
                });
            }).get();
        } catch (InterruptedException e ) {


        }
        catch(ExecutionException e){

        }
    }
}

