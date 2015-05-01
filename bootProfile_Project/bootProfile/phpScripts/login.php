<?php
include 'dbConnection.php';
$var = array();
$isValid=true;
$isNotValid=false;
$sql="";
if(isset($_GET["username"])&&isset($_GET["password"])){
$username=$_GET["username"];
$password=$_GET["password"];
$sql = "SELECT * FROM login WHERE username ='".$username."' AND password ='".$password."';";
$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
if(count($var)==0)
	echo '{"authentication":'.json_encode($isNotValid).'}';
else
	echo '{"authentication":'.json_encode($isValid).',"userProfile":'.json_encode($var).'}';
}
else{
echo '{"authentication":'.json_encode($isNotValid).'}';
}


?>