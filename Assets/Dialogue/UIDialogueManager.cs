using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueManager : MonoBehaviour
{
    public Text text;
    public Animator state;

    public RectTransform buttonParent;

    public GameObject responsePrefab;

    private List<GameObject> last_responses = new List<GameObject>();

    public void Display(string message, string[] responses)
    {
        Show();

        text.text = message;

        //Remove old responses
        foreach(GameObject response in last_responses)
        {
            Destroy(response);
        }
        last_responses.Clear();

        //Add new responses
        for(int i = 0; i < responses.Length; i++)
        {
            GameObject o = Instantiate(responsePrefab, buttonParent);

            //Lerp its position similar to VerticalLayoutGroup
            if (responses.Length > 1)
            {
                o.transform.localPosition = new Vector3(
                    o.transform.localPosition.x,
                    Mathf.Lerp(buttonParent.GetComponent<RectTransform>().rect.yMax, buttonParent.GetComponent<RectTransform>().rect.yMin, (i+0.5f)/responses.Length),
                    0
                );
            }

            //Set the text on the button
            o.transform.GetChild(0).GetComponent<Text>().text = responses[i];

            //Add the callback
            o.GetComponent<Button>().onClick.AddListener(() => ButtonCallback(o));

            //Register for later deletion
            last_responses.Add(o);
        }
    }

    public GameObject showHideTarget;

    public void Show()
    {
        showHideTarget.active = true;
    }

    public void Hide()
    {
        showHideTarget.active = false;
        //Remove old responses
        foreach (GameObject response in last_responses)
        {
            Destroy(response);
        }
        last_responses.Clear();
    }

    public void ButtonCallback(GameObject o)
    {
        int id = -1;
        for (int i = 0; i < last_responses.Count; i++) { if (last_responses[i] == o) id = i; }

        state.SetInteger("responseIndex", id);
        state.SetTrigger("hasResponse");
    }
}
