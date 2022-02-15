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
public class Playlists { //Business entity layer of Playlists
    
    private String name;

    public String getName() { //Returns a name of playlist
        return name;
    }

    public void setName(String name) { //Sets a name of the playlist
        this.name = name;
    }

    private int id;

    public int getId() { //Returns an Id of the playlist
        return id;
    }

    public void setId(int id) { //Sets an Id of the playlist
        this.id = id;
    }

    @Override
    public String toString() { //Converts to String
        return "Playlists{" + "name=" + name + '}';
    }

}
