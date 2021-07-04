<?php
    if(isset($_POST['login'])) {
        include("../php/database.php");
        
        $check_login = $pdo->prepare("SELECT * FROM `accounts` WHERE `login` = ?");
        $check_login->execute([$_POST['login']]);
        $user = $check_login->fetch();
        
        if ($user && password_verify($_POST['pw'], $user['pw']))
        {
            $login_info_array = array('account_id' => $user['id'], 'login' => $user['login'], 'creation_date' => $user['creation_date']);
            $login_info_json = json_encode($login_info_array);
            echo $login_info_json;
        } else {
            echo "0"; //error
        }
        
    } else {
        echo '
        
        <html>
            <head>
                <title>Bruteforce - RPG</title>
            </head>
            <body>
                <form  method="POST" id="loginform" action="index.php">
                    Login: <input type="text" name="login"/>
                    <br>
                    Password: <input type="password" name="pw" /> 
                    <br>
                    <input type="submit" name="submit" value="login" />
                </form>
            </body>
        </html>

        
        ';
    }
?>