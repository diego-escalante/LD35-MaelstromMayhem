using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Movement))]

public class PlantAI : BaseAI {

  private void Awake() {
    weakTag = "Water Elemental";
    strongTag = "Fire Elemental";
    attackRange = 2f;
  }
}

