using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Movement))]

public class WaterAI : BaseAI {

  private void Awake() {
    weakTag = "Fire Elemental";
    strongTag = "Plant Elemental";
    attackRange = 5f;
  }
}
