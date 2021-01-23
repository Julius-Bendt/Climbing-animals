using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimalDatabase : MonoBehaviour
{
    [SerializeField]
    private List<Animal> animals = new List<Animal>();

    private int _id;

    public Animal GetCurrentAnimal()
    {
        return animals[_id];
    }

    public void ChangeSelected(int id)
    {

    }

    public Animal GetAnimal(int id)
    {
        return animals[id];
    }

}
