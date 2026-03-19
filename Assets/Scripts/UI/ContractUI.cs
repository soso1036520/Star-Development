using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContractUI : MonoBehaviour
{
    public Image portrait;
    public TMP_Text nameText;
    public TMP_Text infoText;
    public TMP_Text feeText;

    private ArtistData currentArtist;
    private CompanyData companyData;

    public void Init(ArtistData artist, CompanyData company)
    {
        gameObject.SetActive(true);

        currentArtist = artist;
        companyData = company;

        portrait.sprite = artist.portrait;
        nameText.text = artist.artistName;

        infoText.text =
            $"性別: {artist.gender}\n" +
            $"背景: {artist.background}\n" +
            $"個性: {artist.personality}\n" +
            $"擅長: {artist.specialty}";

        feeText.text = $"簽約金: {artist.signingFee}";
    }

    public void OnClick_Sign()
    {
        companyData.artists.Add(currentArtist);

        Debug.Log("簽約成功：" + currentArtist.artistName);

        gameObject.SetActive(false);
    }

    public void OnClick_Cancel()
    {
        Debug.Log("取消簽約");

        gameObject.SetActive(false);
    }
}