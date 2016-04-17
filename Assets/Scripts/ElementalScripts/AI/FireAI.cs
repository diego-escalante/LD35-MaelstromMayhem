using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Movement))]

public class FireAI : BaseAI {

  private void Awake() {
    weakTag = "Plant Elemental";
    strongTag = "Water Elemental";
    attackRange = 3f;
  }
}
