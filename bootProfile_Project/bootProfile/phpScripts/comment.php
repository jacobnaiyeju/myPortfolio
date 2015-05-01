<?php
include 'dbConnection.php';

if(isset($_POST["Bp_contact_firstname"])
	&&isset($_POST["Bp_contact_surname"])
	&&isset($_POST["bP_cP_messageType"])
	&&isset($_POST["bP_cP_comment"]))
	{
		$firstname=$_POST["Bp_contact_firstname"];
		$surname=$_POST["Bp_contact_surname"];
		$messageType=$_POST["bP_cP_messageType"];
		$comment=$_POST["bP_cP_comment"];
		$sql = "INSERT INTO bootprofile.comment(foreName,surName,messageType,comment) VALUES('".$firstname."','".$surname."','".$messageType."','".$comment."');";
		mysqli_query($con, $sql);
		header('Location: ../contactus.html');
	}
?>