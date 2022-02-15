/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mytunes.BE;

/**
 *
 * @author Arman
 */
public class Songs { //Business entity layer for Songs

    private String title;
    private String artist;
    private String category;
    private String time;



    public String getTitle() { //Returns a title
        return title;
    }

    public void setTitle(String title) { //Set a title
        this.title = title;
    }


    public String getArtist() { //Returns an artist
        return artist;
    }

    public void setArtist(String artist) { //Sets an artist
        this.artist = artist;
    }

    public String getCategory() { // Returns a category
        return category;
    }

    public void setCategory(String category) { //Sets a category
        this.category = category;
    }

    
   
    public String getTime() { //Returns time 
        return time;
    }

    public void setTime(String time) { //Sets the time
        this.time = time;
    }

    private String file;

    public String getFile() { //Returns a file
        return file;
    }

    public void setFile(String file) { //Sets a file
        this.file = file;
    }
    private int id;

    public int getId() { //Returns an Id of a song
        return id;
    }

    public void setId(int id) { //Sets an Id
        this.id = id;
    }


    @Override
    public String toString() { //Converts to string
        return "Songs{" + "title=" + title + ", artist=" + artist + ", category=" + category + ", time=" + time + ", file=" + file + '}';
    }

   

    
}
