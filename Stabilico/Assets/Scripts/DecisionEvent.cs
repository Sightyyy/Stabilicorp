using System;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEvent
{
    public string eventName; // Name of the event
    public string description; // Description of the event
    public Choice choice1; // First choice
    public Choice choice2; // Second choice
    public Choice choice3;
    public Choice choice4;

    public DecisionEvent(string eventName, string description, Choice choice1, Choice choice2, Choice choice3, Choice choice4)
    {
        this.eventName = eventName;
        this.description = description;
        this.choice1 = choice1;
        this.choice2 = choice2;
        this.choice3 = choice3;
        this.choice4 = choice4;
    }
}

public class Choice
{
    public string choiceText; // Text displayed on the button
    public Dictionary<string, int> statChanges; // Stat changes for this choice

    public Choice(string choiceText, Dictionary<string, int> statChanges)
    {
        this.choiceText = choiceText;
        this.statChanges = statChanges;
    }

}