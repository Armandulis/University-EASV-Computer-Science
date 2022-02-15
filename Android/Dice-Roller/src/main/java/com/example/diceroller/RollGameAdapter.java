package com.example.diceroller;

import android.app.Activity;
import android.content.Context;
import android.text.Layout;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.example.diceroller.models.RolledGame;

import java.util.ArrayList;

public class RollGameAdapter extends BaseAdapter {
    Activity context;
    ArrayList<RolledGame> listRolledGames;
    private static LayoutInflater inflater = null;

    public RollGameAdapter(Activity context, ArrayList<RolledGame> listRolledGames){
        this.context = context;
        this.listRolledGames = listRolledGames;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }

    @Override
    public int getCount() {
        return listRolledGames.size();
    }

    @Override
    public RolledGame getItem(int position) {
        return listRolledGames.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View itemView = convertView;
        itemView = (itemView == null) ? inflater.inflate(R.layout.dice_history, null) : itemView;

        RolledGame selectedGame = listRolledGames.get(position);

        TextView textViewnNumbers = (TextView) itemView.findViewById(R.id.textViewNumbers);
        textViewnNumbers.setText(selectedGame.getRolledNumber().toString());

        TextView textViewTime = (TextView) itemView.findViewById(R.id.textViewTime);
        textViewTime.setText(selectedGame.getDate());




        return itemView;
    }
}
