package com.example.diceroller;

import android.content.Intent;
import android.os.PersistableBundle;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import com.example.diceroller.models.RolledGame;

import org.w3c.dom.Text;

import java.util.ArrayList;

public class HistoryActivity extends AppCompatActivity {

    private ArrayList<RolledGame> listHistory;
    private RollGameAdapter adapter;
    ListView listViewHistory;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_history);

        listViewHistory = findViewById(R.id.listHistory);

        Intent intent = getIntent();
        listHistory = (ArrayList<RolledGame>)intent.getSerializableExtra("history" );

        setUpHistory();
    }

    //ONLY SHOWING LAST ROLLED GAME NUMBERS, WHY?
    private void setUpHistory(){
        adapter = new RollGameAdapter(this, listHistory);
        listViewHistory.setAdapter(adapter);

    }

    public void clearHistoryList(View view){
        listHistory.clear();
        listViewHistory.setAdapter(null);
        Intent intent = new Intent();
        intent.putExtra("historyBack", listHistory);
        setResult(2, intent);
        finish();
    }
}
