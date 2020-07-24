using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionDatabase : MonoBehaviour
{
    public static MissionDatabase Instance;
    
    private List<string> _SequenceNames = new List<string>();
    private List<Mission> _Missions = new List<Mission>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddSequence(string sequence)
    {
        _SequenceNames.Add(sequence);
    }

    public void AddMission(Mission mis)
    {
        _Missions.Add(mis);
    }

    public List<string> GetSequences() => _SequenceNames;

    public bool FindSequence(string seq)
    {
        if (_SequenceNames.Contains(seq))
            return true;

        return false;
    }

    public bool FindMission(Mission mis)
    {
        if (_Missions.Contains(mis))
            return true;

        return false;
    }

}
