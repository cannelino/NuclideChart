using OVR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Decayrow : MonoBehaviour
{
    public enum Rows { Francium, Thorium, Neptunium, UranRadium, UranActinium};
    public enum Scene { FloorWalk, FloorTeleport, WallWalk, WallTeleport };
    public Rows ActiveRow;
    public Scene ActiveScene;
    public Transform Player;
    public Transform centreCamera;
    public NuclidDictionary nucliddata;
    private List<KeyClassDict> rowelements = new List<KeyClassDict>();
    public SoundFXRef wrongSound;
    public SoundFXRef rightSound;
    public TextMeshProUGUI timetextbox;
    public TextMeshProUGUI errortextbox;
    public TextMeshProUGUI correcttextbox;
    public TextMeshProUGUI overlaytextbox;
    public GameObject OverlayCanvas;
    private int errorcount = 0;
    private DateTime startTime;
    public bool hasStarted = false;
    private int correctCount = 0;
    public int interactionCount = 0;
    public WalkSelection walkSelection;
    public bool isFloor;
    public Transform nuclidParent;
    public bool isTraining = false;
    public StudyManager studyManager;

    private void Start()
    {
        ActiveRow = studyManager.activeRow;
    }

    public void SetStart()
    {
        errortextbox.text = "Fehler: " + errorcount.ToString();
        correcttextbox.text = correctCount + " / 6";
        Nuklidclass Originnuklid = new Nuklidclass();
        switch (ActiveRow)
        {
            case Rows.Francium:

                if (nucliddata.nucliddata.TryGetValue(new KeyClassDict(86, 133), out Originnuklid))
                {
                    //Originnuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                    //Player.position = new Vector3(Originnuklid.Prefab.transform.position.x - centreCamera.localPosition.x, 0.65f, Originnuklid.Prefab.transform.position.z - centreCamera.localPosition.z);
                    StartCoroutine(SetPositionAfterTime(0.1f, Originnuklid, new KeyClassDict(86, 133)));
                }

                break;
            case Rows.Thorium:

                if (nucliddata.nucliddata.TryGetValue(new KeyClassDict(85, 134), out Originnuklid))
                {
                    //Originnuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                    //Player.position = new Vector3(Originnuklid.Prefab.transform.position.x - centreCamera.localPosition.x, 0.65f, Originnuklid.Prefab.transform.position.z - centreCamera.localPosition.z);
                    StartCoroutine(SetPositionAfterTime(0.1f, Originnuklid, new KeyClassDict(85, 134)));
                }

                break;
            case Rows.Neptunium:

                if (nucliddata.nucliddata.TryGetValue(new KeyClassDict(84, 132), out Originnuklid))
                {
                    //Originnuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                    //Player.position = new Vector3(Originnuklid.Prefab.transform.position.x - centreCamera.localPosition.x, 0.65f, Originnuklid.Prefab.transform.position.z - centreCamera.localPosition.z);
                    StartCoroutine(SetPositionAfterTime(0.1f, Originnuklid, new KeyClassDict(84, 132)));
                }
                break;
            case Rows.UranRadium:

                if (nucliddata.nucliddata.TryGetValue(new KeyClassDict(82, 131), out Originnuklid))
                {
                    //Originnuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                    //Player.position = new Vector3(Originnuklid.Prefab.transform.position.x - centreCamera.localPosition.x, 0.65f, Originnuklid.Prefab.transform.position.z - centreCamera.localPosition.z);
                    StartCoroutine(SetPositionAfterTime(0.1f, Originnuklid, new KeyClassDict(82, 131)));
                }
                break;
            case Rows.UranActinium:

                if (nucliddata.nucliddata.TryGetValue(new KeyClassDict(85, 133), out Originnuklid))
                {
                    //Originnuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                    //Player.position = new Vector3(Originnuklid.Prefab.transform.position.x - centreCamera.localPosition.x, 0.65f, Originnuklid.Prefab.transform.position.z - centreCamera.localPosition.z);
                    StartCoroutine(SetPositionAfterTime(0.1f, Originnuklid, new KeyClassDict(85, 133)));
                }
                break;
            default:
                break;
        }
        SetRowElements();
        HighlightFirst();
        //HighlightRow();
        overlaytextbox.text = "Starten - durch Auswahl des Startnuklids";

    }

    private void SetRowElements()
    {
        switch (ActiveRow)
        {
            case Rows.Francium:
                rowelements.Add(new KeyClassDict(87, 133));
                rowelements.Add(new KeyClassDict(88, 132));
                rowelements.Add(new KeyClassDict(85, 131));
                rowelements.Add(new KeyClassDict(86, 130));
                rowelements.Add(new KeyClassDict(83, 129));
                rowelements.Add(new KeyClassDict(84, 128));
                rowelements.Add(new KeyClassDict(81, 127));
                rowelements.Add(new KeyClassDict(82, 126));

                break;
            case Rows.Thorium:

                //rowelements.Add(new KeyClassDict(90, 142));
                //rowelements.Add(new KeyClassDict(88, 140));
                //rowelements.Add(new KeyClassDict(89, 139));
                //rowelements.Add(new KeyClassDict(90, 138));
                //rowelements.Add(new KeyClassDict(88, 136));
                rowelements.Add(new KeyClassDict(86, 134));
                rowelements.Add(new KeyClassDict(84, 132));
                rowelements.Add(new KeyClassDict(82, 130));
                rowelements.Add(new KeyClassDict(83, 129));
                rowelements.Add(new KeyClassDict(81, 127));
                rowelements.Add(new KeyClassDict(84, 128));
                rowelements.Add(new KeyClassDict(82, 126));

                break;
            case Rows.Neptunium:

                //rowelements.Add(new KeyClassDict(93, 144));
                //rowelements.Add(new KeyClassDict(91, 142));
                //rowelements.Add(new KeyClassDict(92, 141));
                //rowelements.Add(new KeyClassDict(90, 139));
                //rowelements.Add(new KeyClassDict(88, 137));
                //rowelements.Add(new KeyClassDict(89, 136));
                //rowelements.Add(new KeyClassDict(87, 134));
                rowelements.Add(new KeyClassDict(85, 132));
                rowelements.Add(new KeyClassDict(83, 130));
                rowelements.Add(new KeyClassDict(84, 129));
                rowelements.Add(new KeyClassDict(81, 128));
                rowelements.Add(new KeyClassDict(82, 127));
                rowelements.Add(new KeyClassDict(83, 126));
                rowelements.Add(new KeyClassDict(81, 124));

                break;
            case Rows.UranRadium:

                //rowelements.Add(new KeyClassDict(92, 146));
                //rowelements.Add(new KeyClassDict(90, 144));
                //rowelements.Add(new KeyClassDict(91, 143));
                //rowelements.Add(new KeyClassDict(92, 142));
                //rowelements.Add(new KeyClassDict(90, 140));
                //rowelements.Add(new KeyClassDict(88, 138));
                //rowelements.Add(new KeyClassDict(86, 136));
                //rowelements.Add(new KeyClassDict(84, 134));
                //rowelements.Add(new KeyClassDict(82, 132));
                rowelements.Add(new KeyClassDict(83, 131));
                rowelements.Add(new KeyClassDict(84, 130));
                rowelements.Add(new KeyClassDict(81, 129));
                rowelements.Add(new KeyClassDict(82, 128));
                rowelements.Add(new KeyClassDict(83, 127));
                rowelements.Add(new KeyClassDict(84, 126));
                rowelements.Add(new KeyClassDict(82, 124));

                break;
            case Rows.UranActinium:

                //rowelements.Add(new KeyClassDict(92, 143));
                //rowelements.Add(new KeyClassDict(90, 141));
                //rowelements.Add(new KeyClassDict(91, 140));
                //rowelements.Add(new KeyClassDict(89, 138));
                //rowelements.Add(new KeyClassDict(90, 137));
                //rowelements.Add(new KeyClassDict(87, 136));
                //rowelements.Add(new KeyClassDict(88, 135));
                rowelements.Add(new KeyClassDict(86, 133));
                rowelements.Add(new KeyClassDict(84, 131));
                rowelements.Add(new KeyClassDict(82, 129));
                rowelements.Add(new KeyClassDict(83, 128));
                rowelements.Add(new KeyClassDict(84, 127));
                rowelements.Add(new KeyClassDict(81, 126));
                rowelements.Add(new KeyClassDict(82, 125));

                break;
            default:
                break;
        }
    }

    private void HighlightRow()
    {
        Nuklidclass nuklid = new Nuklidclass();
        foreach (KeyClassDict key in rowelements)
        {
            if (nucliddata.nucliddata.TryGetValue(key, out nuklid))
            {
                nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                if (isFloor) nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetRaise(true);
            }
        }
    }

    private void HighlightFirst()
    {
        Nuklidclass nuklid = new Nuklidclass();
        if (nucliddata.nucliddata.TryGetValue(rowelements[0], out nuklid))
        {
            nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
            if (isFloor) nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetRaise(true);

        }
        
    }

    public bool IsInRow(KeyClassDict key)
    {
        return rowelements.Contains(key);
    }

    private bool IsFirst(KeyClassDict key)
    {
        return rowelements[0].Equals(key);
    }

    private bool IsLast(KeyClassDict key)
    {
        return rowelements[rowelements.Count-1].Equals(key);
    }


    public void checkSelected(KeyClassDict key)
    {
        //interactionCount++;
        Nuklidclass nuklid = new Nuklidclass();
        if (nucliddata.nucliddata.TryGetValue(key, out nuklid))
        {
            if(nuklid.Prefab.GetComponent<NuclidPrefabElements>().selected == false)
            {
                if (hasStarted) nuklid.Prefab.GetComponent<NuclidPrefabElements>().selected = true;
                if (IsInRow(key))
                {
                    if (IsFirst(key) && !hasStarted)
                    {
                        hasStarted = true;
                        startTime = DateTime.Now;
                        if (!isTraining) OverlayCanvas.SetActive(false);
                        nuklid.Prefab.GetComponent<NuclidPrefabElements>().selected = true;
                    }
                    else if(IsLast(key)&& hasStarted)
                    {
                        hasStarted = false;
                        
                        if (isFloor) nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetRaise(true);
                        rightSound.PlaySoundAt(nuklid.Prefab.transform.position);
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
                        StartCoroutine(StopVibrationAfterTime(0.5f));
                        correctCount++;
                        correcttextbox.text = correctCount + " / 6";
                        if (!isTraining) logData();
                        if (!isTraining) OverlayCanvas.SetActive(true);
                        overlaytextbox.text = "Ende erreicht \n Anzahl Fehler: " + errorcount + "\n" + getTime();
                        nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);


                    }
                    if (hasStarted) { 
                        nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.green);
                        if (isFloor) nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetRaise(true);
                        rightSound.PlaySoundAt(nuklid.Prefab.transform.position);
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
                        StartCoroutine(StopVibrationAfterTime(0.5f));
                        correctCount++;
                        correcttextbox.text = correctCount + " / 6";
                    }
                }
                else if(hasStarted)
                {
                    nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.red);
                    if (isFloor) nuklid.Prefab.GetComponent<NuclidPrefabElements>().SetRaise(false);
                    wrongSound.PlaySoundAt(nuklid.Prefab.transform.position);
                    OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
                    OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
                    StartCoroutine(StopVibrationAfterTime(0.2f));
                    StartCoroutine(StartVibrationAfterTime(0.25f));
                    StartCoroutine(StopVibrationAfterTime(0.45f));
                    errorcount++;
                    errortextbox.text = "Fehler: " + errorcount.ToString();
                }
            }
        }
    }

    private void logData()
    {
        string fname = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".csv";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter file = new StreamWriter(path);
        file.WriteLine("ID;SceneName;Decayrow;StartTime;TotalTime;Errors;Interactions");
        string log = "";
        log += studyManager.id + ";";
        log += ActiveScene + ";";
        log += ActiveRow + ";";
        log += startTime.ToString("dd/MM/yyyy HH:mm:ss") + ";";
        log += (DateTime.Now - startTime).TotalSeconds + ";";
        log += errorcount + ";";
        log += interactionCount;
        file.WriteLine(log);
        file.Close();
    }

    private void Update()
    {
        if (hasStarted)
        {
            timetextbox.text = getTime();
        }
    }

    private string getTime()
    {
        TimeSpan elapsedTime = DateTime.Now - startTime;
        String minute = "";
        if (elapsedTime.Minutes > 10) minute = elapsedTime.Minutes.ToString();
        else minute = "0" + elapsedTime.Minutes.ToString();
        String seconds = "";
        if (elapsedTime.Seconds > 10) seconds = elapsedTime.Seconds.ToString();
        else seconds = "0" + elapsedTime.Seconds.ToString();
        return "Zeit: " + minute + ":" + seconds;

    }

IEnumerator StopVibrationAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    IEnumerator StartVibrationAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
    }

    IEnumerator SetPositionAfterTime(float time, Nuklidclass Originnuklid, KeyClassDict start)
    {
        yield return new WaitForSeconds(time);

        if (isFloor)
        {
            Nuklidclass firstNuklid = new Nuklidclass();
            if (nucliddata.nucliddata.TryGetValue(rowelements[0], out firstNuklid))
            {
                firstNuklid.Prefab.transform.parent = null;
                nuclidParent.parent = firstNuklid.Prefab.transform;
                firstNuklid.Prefab.transform.position = new Vector3(100.5f, 0.075f, 64.5f);
                nuclidParent.parent = null;
                firstNuklid.Prefab.transform.parent = nuclidParent;
            }

            Player.position = new Vector3(Originnuklid.Prefab.transform.position.x - centreCamera.localPosition.x, 0.2f, Originnuklid.Prefab.transform.position.z - centreCamera.localPosition.z);
            walkSelection.setnuclidstart(start);
        }
        else
        {
            Player.rotation = Quaternion.Euler(0, 90, 0);
            if (ActiveScene == Scene.WallTeleport)
            {
                Player.position = new Vector3(113.73f, 0, 66.7f);

            }
            else
            {
                Player.position = new Vector3(115.0f, 0, 66.7f);
                nuclidParent.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }

            Nuklidclass firstNuklid = new Nuklidclass();
            if (nucliddata.nucliddata.TryGetValue(rowelements[0], out firstNuklid))
            {
                firstNuklid.Prefab.transform.parent = null;
                nuclidParent.parent = firstNuklid.Prefab.transform;
                firstNuklid.Prefab.transform.position = new Vector3(115.65f, 1.4f, 66.7f);
                nuclidParent.parent = null;
                firstNuklid.Prefab.transform.parent = nuclidParent;
            }

    }
}


}