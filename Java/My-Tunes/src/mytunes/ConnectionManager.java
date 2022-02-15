/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package mytunes;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import com.microsoft.sqlserver.jdbc.SQLServerException;
import java.sql.Connection;

/**
 *
 * @author Arman
 */
public class ConnectionManager { //Class for connecting to the database
    
    
    private SQLServerDataSource ds = new SQLServerDataSource();
    
    public ConnectionManager() { //User details for connecting to the database
        ds.setDatabaseName("nice");
        ds.setUser("CS2017B_20");
        ds.setPassword("CS2017B_20");
        ds.setPortNumber(1433);
        ds.setServerName("10.176.111.31");
    }
    
    
     public Connection getConnection() throws SQLServerException //Initializes the connection to the database
    {
        return ds.getConnection();
    }
     
     
    
}
