using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
<<<<<<< HEAD
using UnityEngine.EventSystems;
=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
=======
using UnityEngine.EventSystems;
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f

// Code heavily adapted from the YouTube tutorials: https://www.youtube.com/watch?v=OL1QgwaDsqo
// This SelectionAgent uses the box collider to detect whether some GameObjects are selected
// When some GameObjects collide with the Mesh Collider created by the drawing a box in the screen,
// OnTriggerEnter() method will be called and add all GameObjects that collide with to the SelectionDictionary

public class SelectionAgent : MonoBehaviour
{
    RaycastHit hit;

    bool dragSelect;

    LayerMask selectableLayer;

    //Collider variables
    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1;
    Vector3 p2;

    //the corners of our 2d selection box
    Vector2[] corners;

    //the vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;

    // Start is called before the first frame update
    void Start()
    {
        dragSelect = false;
<<<<<<< HEAD
<<<<<<< HEAD
        // "Selectable" layer
=======
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
=======
        // "Selectable" layer
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f
        selectableLayer = 1 << 7;
    }

    // Update is called once per frame
    void Update()
    {
        //1. when left mouse button clicked (but not released)
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }

        //2. while left mouse button held
        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }
        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(0))
        {
            if (!dragSelect) //single select
            {
                Ray ray = Camera.main.ScreenPointToRay(p1);

<<<<<<< HEAD
<<<<<<< HEAD
                if(!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
                    {
                        // If user click on anything
                        // or the ray hits anything
                        GameObject objHit = hit.transform.gameObject;

                        // If user clicks on anything that is selectable
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            // holding left shift to add the unit to the current selection
                            SelectionDictionary.addSelected(objHit);
                        }
                        else
                        {
                            // If not holding left shift, all current selected objects will be deselected
                            // then add the object hit to the selection dictinoary


                            SelectionDictionary.deselectAll();
                            SelectionDictionary.addSelected(objHit);
                        }
                    }
                    else
                    {
                        // If user does not click on any selectable object, every thing selected will be deselected

                        SelectionDictionary.deselectAll();
                    }
                }
=======
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
=======
                if(!EventSystem.current.IsPointerOverGameObject())
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
                    {
                        // If user click on anything
                        // or the ray hits anything
                        GameObject objHit = hit.transform.gameObject;

                        // If user clicks on anything that is selectable
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            // holding left shift to add the unit to the current selection
                            SelectionDictionary.addSelected(objHit);
                        }
                        else
                        {
                            // If not holding left shift, all current selected objects will be deselected
                            // then add the object hit to the selection dictinoary


                            SelectionDictionary.deselectAll();
                            SelectionDictionary.addSelected(objHit);
                        }
                    }
                    else
                    {
                        // If user does not click on any selectable object, every thing selected will be deselected

                        SelectionDictionary.deselectAll();
                    }
                }
<<<<<<< HEAD
                else
                {
                    // If user does not click on any selectable object, every thing selected will be deselected
                    SelectionDictionary.deselectAll();
                }
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
=======
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f
            }
            else //marquee select
            {
                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;
                p2 = Input.mousePosition;
                corners = getBoundingBox(p1, p2);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        vecs[i] = ray.origin - hit.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }

                //generate the mesh
                selectionMesh = generateSelectionMesh(verts, vecs);

                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    SelectionDictionary.deselectAll();
                }

                Destroy(selectionBox, 0.02f);

            }//end marquee select

            dragSelect = false;

        }

        SelectionDictionary.enableIndicater();

    }

    private void OnGUI()
    {
        if (dragSelect == true)
        {
            var rect = DrawingUtils.GetScreenRect(p1, Input.mousePosition);
            DrawingUtils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            DrawingUtils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    //create a bounding box (4 corners in order) from the start and end mouse position
    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        Vector2 newP1;
        Vector2 newP2;
        Vector2 newP3;
        Vector2 newP4;

        if (p1.x < p2.x) //if p1 is to the left of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = p1;
                newP2 = new Vector2(p2.x, p1.y);
                newP3 = new Vector2(p1.x, p2.y);
                newP4 = p2;
            }
            else //if p1 is below p2
            {
                newP1 = new Vector2(p1.x, p2.y);
                newP2 = p2;
                newP3 = p1;
                newP4 = new Vector2(p2.x, p1.y);
            }
        }
        else //if p1 is to the right of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = new Vector2(p2.x, p1.y);
                newP2 = p1;
                newP3 = p2;
                newP4 = new Vector2(p1.x, p2.y);
            }
            else //if p1 is below p2
            {
                newP1 = p2;
                newP2 = new Vector2(p1.x, p2.y);
                newP3 = new Vector2(p2.x, p1.y);
                newP4 = p1;
            }

        }

        Vector2[] corners = { newP1, newP2, newP3, newP4 };
        return corners;

    }

    //generate a mesh from the 4 bottom points
    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f
        if (other.gameObject.GetComponent<UnitScript>())
        {
            SelectionDictionary.addSelected(other.gameObject);
        }
        
<<<<<<< HEAD
=======
        SelectionDictionary.addSelected(other.gameObject);
>>>>>>> 0f8ac105f7446494ace63d341113757fe1908527
=======
>>>>>>> e50c5c28b8cbf917ee55e27f3a12250adbe2452f
    }

}