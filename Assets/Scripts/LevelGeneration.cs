using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelGeneration : MonoBehaviour
{
    public GameObject cube;
    public GameObject ball;
    public GameObject level_ground;
    public GameObject[] obstacles;
    public int levels_on_screen;

    private List<GameObject> levels = new List<GameObject>();
    private List<GameObject> obstacles_on_screen = new List<GameObject>();

    private int current_level;
    private Color purple_glass = new Color(0.7688852f, 0, 0.8773585f, 0.2352941f);
    private bool rotate_next = false;

    public Text level_text;
    public Material glass_material;
    public Material obstacle_material;
    public Material solid_red;

    // Start is called before the first frame update
    void Start()
    {
        level_text.text = "Current Level: 0";

        for (int i = 0; i < levels_on_screen; i++)
        {
            levels.Add(Instantiate(level_ground, new Vector3(0, i * (-level_ground.transform.localScale.y) * 1.5f, 0), Quaternion.identity, cube.transform));
            if (rotate_next)
                levels[i].transform.localRotation = Quaternion.Euler(0, 180, 0);
            rotate_next = !rotate_next;

            int obstacle_i = Random.Range(0, obstacles.Length);
            obstacles_on_screen.Add(Instantiate(obstacles[obstacle_i], new Vector3(0.5f, i * (-level_ground.transform.localScale.y) * 1.5f, 0), Quaternion.identity, cube.transform));
        }

        ball = Instantiate(ball, new Vector3(0, (levels_on_screen/2 * (-level_ground.transform.localScale.y) * 1.5f) + 0.35f, 0), Quaternion.identity);
        ChangeColor(levels_on_screen / 2, solid_red, obstacle_material);
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.GetComponent<BallScript>().EnteredTrigger)
        {
            ball.GetComponent<BallScript>().EnteredTrigger = false;
            ChangeColor(levels_on_screen/2, glass_material, glass_material);
            current_level++;
            ChangeColor(levels_on_screen/2 + 1, solid_red, obstacle_material);

            // Add new level and remove the old one
            Destroy(levels[0]);
            levels.RemoveAt(0);
            Destroy(obstacles_on_screen[0]);
            obstacles_on_screen.RemoveAt(0);
            cube.transform.Translate(Vector3.up * level_ground.transform.localScale.y * 1.5f, Space.Self);
            levels.Add(Instantiate(level_ground, new Vector3(0, 0, 0), new Quaternion(0,0,0,0), cube.transform));
            if (rotate_next)
                levels.LastOrDefault().transform.localRotation = Quaternion.Euler(0, 180, 0);
            levels.LastOrDefault().transform.localPosition = Vector3.zero;
            float next_level_y = ((levels_on_screen + current_level) * (-level_ground.transform.localScale.y) * 1.5f) + 1.5f;
            levels.LastOrDefault().transform.Translate(new Vector3(0, next_level_y, 0), Space.Self);
            rotate_next = !rotate_next;

            int obstacle_i = Random.Range(0, obstacles.Length);
            obstacles_on_screen.Add(Instantiate(obstacles[obstacle_i], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), cube.transform));
            obstacles_on_screen.LastOrDefault().transform.localPosition = Vector3.zero;
            obstacles_on_screen.LastOrDefault().transform.Translate(new Vector3(0.5f, next_level_y, 0), Space.Self);

            level_text.text = "Current Level: " + current_level;
        }
    }
    
    void ChangeColor(int level, Material ground_mat, Material obstacle_material)
    {
        for (int i = 0; i < 2; i++)
        {
            levels[level].transform.GetChild(i).gameObject.GetComponent<Renderer>().material = ground_mat;
        }
        for (int i = 0; i < obstacles_on_screen[level].transform.childCount; i++)
        {
            obstacles_on_screen[level].transform.GetChild(i).GetComponent<Renderer>().material = obstacle_material;
        }
    }
}
