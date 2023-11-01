using TMN;

public class TestEventWithoutParameter : AEvent
{
}

public class TestEventWithParameter : AEvent<bool>
{
}

public class ChangeCurrency : AEvent
{
}

public class BuyCard : AEvent<int>
{
}

public class ThrowCard : AEvent
{
}

public class CollectGift : AEvent
{
}

public class GiftCollected : AEvent<int>
{
}


public class CardDisabled : AEvent<Card, bool>
{
}

public class AllCardsDisabled : AEvent
{
}