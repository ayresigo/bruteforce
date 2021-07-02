using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    /* account_infoArr =    #
     * 1,                   0
     * login,               1
     * creation_date        2
     */

    [SerializeField]
    [ReadOnly]
    private string _login;

    public string login
    {
        get { return _login; }
        set { _login = value; }
    }

    [SerializeField]
    [ReadOnly]
    private string _creation_date;

    public string creation_date
    {
        get { return _creation_date; }
        set { _creation_date = value; }
    }

}
