<?php
    if(isset($_POST['account_id'])) {
        include("../php/database.php");
        
        $get_inventory = $pdo->prepare("SELECT * FROM `characters` WHERE `fk_owner_id` = ?");
        $get_inventory->execute([$_POST['account_id']]);
        $inventory_info_array = $get_inventory->fetchAll();
        
        echo json_encode($inventory_info_array);
        
    } else {
        echo '
        
        <html>
            <head>
                <title>Bruteforce - RPG</title>
            </head>
            <body>
                <form  method="POST" id="loginform" action="get_inventory.php">
                    Account ID: <input type="text" name="account_id"/>
                    <br>
                    <input type="submit" name="submit" value="login" />
                </form>
            </body>
        </html>

        
        ';
    }
?>