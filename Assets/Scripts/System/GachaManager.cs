using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public ContractUI contractUI;
    public CompanyData companyData;

    public Sprite testSprite;

    public void DrawArtist()
    {
        ArtistData artist = ArtistGenerator.GenerateRandomArtist(testSprite);

        contractUI.Init(artist, companyData);
    }
}