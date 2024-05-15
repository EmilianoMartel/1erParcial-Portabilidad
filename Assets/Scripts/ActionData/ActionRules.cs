using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRules
{
    public ActionData actionData;
}

public class MeeleAttack : ActionRules { }

public class RangeAttack : ActionRules { }

public class Health : ActionRules { }

public class SealfHealth : Health { }