using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    public Player Player { get => GetComponent<Player>(); }
    public Rigidbody2D Rigidbody2D { get => GetComponent<Rigidbody2D>(); }
    public Animator Animator { get => GetComponent<Animator>(); }
}
