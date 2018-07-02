using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridCell : MonoBehaviour {
    [SerializeField] float cellSize = 1;
    
	
	void Update () {
        Vector2 gridPosition = new Vector2(
            Mathf.RoundToInt(transform.position.x / cellSize), 
            Mathf.RoundToInt(transform.position.y / cellSize));
        transform.position = gridPosition * cellSize;

        // 100 pixels / pixels of sprite
        // ex 100 / 64 = 1.5625 (to scale in order to get to 1 unit)
        float spriteScaleAdjustement = 1 / GetComponent<SpriteRenderer>().sprite.bounds.size.x * cellSize;
        // Assume sprite is square
        transform.localScale = new Vector3(
            spriteScaleAdjustement,
            spriteScaleAdjustement,
            transform.localScale.z
        );
	}
}
