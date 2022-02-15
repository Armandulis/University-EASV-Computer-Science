package com.example.diceroller.models;


import android.os.Parcel;
import android.os.Parcelable;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class RolledGame implements Serializable {

    private List<Integer> listrolledNumber;
    private String date;

    public RolledGame(List<Integer> rolledNumbers, String dates){
        listrolledNumber = rolledNumbers;
        date = dates;
    }

    public List<Integer> getRolledNumber(){
        return listrolledNumber;
    }

    public String getDate(){
        return date;
    }

    /*
    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeList(this.listrolledNumber);
        dest.writeString(this.date);
    }

    protected RolledGame(Parcel in) {
        this.listrolledNumber = new ArrayList<>();
        in.readList(this.listrolledNumber, Integer.class.getClassLoader());
        this.date = in.readString();
    }

    public static final Parcelable.Creator<RolledGame> CREATOR = new Parcelable.Creator<RolledGame>() {
        @Override
        public RolledGame createFromParcel(Parcel source) {
            return new RolledGame(source);
        }

        public RolledGame[] newArray(int size) {
            return new RolledGame[size];
        }
    };*/
}