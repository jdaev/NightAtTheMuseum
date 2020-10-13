public class Card
{
    public int CardNumber;
    public bool isRed;

    public bool isHidden;

    public Card(int CardNumber, bool isRed, bool isHidden){
        this.CardNumber = CardNumber;
        this.isRed = isRed;
        this.isHidden = isHidden;
    }

    
}