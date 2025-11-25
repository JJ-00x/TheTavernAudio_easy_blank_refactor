using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using FMODUnity;
public class FMOD_Commands : MonoBehaviour
{
    #region EVENT EMITTER
    // EVENT EMITTER
    [SerializeField] public StudioEventEmitter tavernEmitter; // Deklaracja publicznego pola, które przechowuje referencję do event emittera na scenie.
    #endregion

    #region EVENT
    // EVENT
    FMOD.Studio.EventInstance FootstepsSound; // Deklaracja zmiennej, która będzie przechowywać instancję eventu Footsteps.
    public EventReference footstepsEvent; // Deklaracja publicznego pola, które przechowuje referencję do pliku z eventem Footsteps.
    
    private void Footsteps()
    {
        // jednorazowe odtworzenie
        RuntimeManager.PlayOneShot(footstepsEvent); // Odtwarza event jednokrotnie bez zarządzania jego instancją.
        
        // podstawowe zarządzanie eventem
        FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
        //FootstepsSound = FMODUnity.RuntimeManager.CreateInstance("event:/SciezkaDoEventu");
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
        FootstepsSound.setParameterByName("ContinousParametr", 1);
        FootstepsSound.start();
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FootstepsSound.release();

        // zarządzanie eventem z przypięciem emittera do gameObjectu 
        FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
        FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
        FootstepsSound.start();
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FootstepsSound.release();
    }
    #endregion

    #region SNAPSHOT
    // SNAPSHOT
    FMOD.Studio.EventInstance HealthSnap; // Deklaracja zmiennej, która będzie przechowywać instancję snapshotu Health.
    public EventReference healthSnapshot; // Deklaracja publicznego pola, które przechowuje referencję do pliku z snapshotem Health.

    private void StartSnapshot()
    {
        if (tavernEmitter != null && tavernEmitter.IsPlaying()) // Sprawdza, czy event emitter istnieje i jest aktywny.
        {
            HealthSnap = FMODUnity.RuntimeManager.CreateInstance(healthSnapshot);
            HealthSnap.start();
        }
        else if (tavernEmitter != null && tavernEmitter.IsPlaying())
        {
            HealthSnap.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            HealthSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            HealthSnap.release();
        }
    }
    #endregion

    #region VCA
    // VCA
    FMOD.Studio.VCA GlobalVCA; // Deklaracja zmiennej, która będzie przechowywać referencję do VCA o nazwie "Mute".

    private void VCA()
    {
        GlobalVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Mute");
        GlobalVCA.setVolume(DecibelToLinear(0));
        GlobalVCA.setVolume(DecibelToLinear(-100));
    }

    private float DecibelToLinear(float dB) // Funkcja przeliczająca wartość decybelową na skalę liniową.
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
    #endregion

    #region EVENT / EMITTER Z MUZYKĄ
    // EVENT / EMITTER Z MUZYKĄ
    FMOD.Studio.EventInstance Music; // Deklaracja zmiennej, która będzie przechowywać instancję eventu Music.
    public FMODUnity.StudioEventEmitter tavernEmitter_Music; // Deklaracja publicznego pola, które przechowuje referencję do event emittera na scenie.
    [SerializeField] 
    public EventReference musicEvent; // Deklaracja publicznego pola, które przechowuje referencję do pliku z eventem muzycznym.

    private void MusicSwitch()
    {
        // EVENT
        Music = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        Music.setParameterByNameWithLabel("Switch_parts", "Part 2");
        Music.start();
        Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Music.release();

        // EMITTER
        tavernEmitter_Music.SetParameter("Switch_parts", 0);
        tavernEmitter_Music.Play();
        tavernEmitter_Music.Stop();
    }
    #endregion
}
