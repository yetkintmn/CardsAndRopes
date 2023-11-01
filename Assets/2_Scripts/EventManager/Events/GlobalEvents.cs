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

public class BuyCard : AEvent
{
}

public class ThrowCard : AEvent
{
}

public class CollectGift : AEvent
{
}