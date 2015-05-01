<?php
include 'dbConnection.php';
$var = array();
$sql = "SELECT * FROM league";
$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
echo '{"leagues":'.json_encode($var).'}';
?>