package com.example.diceroller;

import android.content.Intent;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.diceroller.models.RolledGame;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class MainActivity extends AppCompatActivity implements AdapterView.OnItemSelectedListener {

    private int selectedDiceAmount;
    private ArrayList<String> listOfRolledDicesString = new ArrayList<>();
    private ArrayList<RolledGame> listHistory = new ArrayList<>();
    ListView listViewDice;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        listViewDice = findViewById(R.id.listViewRolledDice);
        setUpDropdown();

    }

    private void showToast(String message){
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

        // showing selected amount of dice
        String diceAmount = parent.getItemAtPosition(position).toString();
        showToast(diceAmount);

        // defining Dice amount
      switch (diceAmount){
            case "One dice":
                selectedDiceAmount = 1;
                break;
            case "Two dices":
                selectedDiceAmount = 2;
                break;
            case "Three dices":
                selectedDiceAmount = 3;
                break;
            case "Four dices":
                selectedDiceAmount = 4;
                break;
            case "Five dices":
                selectedDiceAmount = 5;
                break;
            case "Six dices":
                selectedDiceAmount = 6;
                break;
            default:
                selectedDiceAmount = 1;
        }


    }


    @Override
    public void onNothingSelected(AdapterView<?> parent) {

    }


    public void viewHistory(View view){
        Intent intent = new Intent(this, HistoryActivity.class);
        intent.putExtra("history", listHistory);
        startActivityForResult(intent, 2);

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode,  Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(requestCode == 2){
            if(data!=null){
                //Retriving data from History Activity
                listHistory = (ArrayList<RolledGame>)data.getSerializableExtra("historyBack");
                //clearing View if history was cleared
                if(listHistory.size() == 0) {
                    showToast("Rolls have been cleared");
                    clearLists();
                }
            }

        }
    }

    private void setUpListViewString(){
        if(listOfRolledDicesString != null) {
            ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1);
            adapter.addAll(listOfRolledDicesString);
            listViewDice.setAdapter(adapter);
        }
    }

    private void clearLists(){
        // Clearing lists
        listOfRolledDicesString.clear();

        listViewDice.setAdapter(null);


    }

    public void rollDice(View view){

        clearLists();


        int rolledDiceNumber;

        DateFormat dateFormat = new SimpleDateFormat(" HH:mm:ss");
        Date date = new Date();

        ArrayList<Integer> rolls = new ArrayList<>();
        //Rolling dice - generating random numbers and adding them to the list
       for(int i=1; i<=selectedDiceAmount; i++){
           rolledDiceNumber = (int)(Math.random()*6+1);
           listOfRolledDicesString.add("Dice " + i + " rolled: " + rolledDiceNumber);
           rolls.add(rolledDiceNumber);
       }


        setUpListViewString();

        RolledGame dice = new RolledGame(rolls, dateFormat.format(date));
        listHistory.add(dice);

        return;


    }

    private void setUpDropdown(){
        Spinner dropdown = findViewById(R.id.number_of_dice);

        ArrayAdapter<CharSequence> adapter =  ArrayAdapter.createFromResource(this, R.array.dice_amount, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        dropdown.setAdapter(adapter);
        dropdown.setSelection(0);
        dropdown.setOnItemSelectedListener(this);

    }

}
