using UnityEngine;
using MidiParser;
public class RhythmGameManager : MonoBehaviour
{
    string filePath = "Assets/_RhythmGame/Songs/";
    string[] songNames = new string[] { "song1.mid", "song2.mid", "song3.mid" };
    void Start()
    {
    var midiFile = new MidiFile("song.mid");

    // 0 = single-track, 1 = multi-track, 2 = multi-pattern
    var midiFileformat = midiFile.Format;

    // also known as pulses per quarter note
    var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

        foreach(var track in midiFile.Tracks)
        {
            foreach(var midiEvent in track.MidiEvents)
            {
                if(midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    var channel = midiEvent.Channel;
                    var note = midiEvent.Note;
                    var velocity = midiEvent.Velocity;
                }
            }

            foreach(var textEvent in track.TextEvents)
            {
                if(textEvent.TextEventType == TextEventType.Lyric)
                {
                    var time = textEvent.Time;
                    var text = textEvent.Value;
                }
            }    
        }
    }

    
}
