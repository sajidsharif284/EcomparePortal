<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Api extends CI_Controller {

	/**
	 * Index Page for this controller.
	 *
	 * Maps to the following URL
	 * 		http://example.com/index.php/welcome
	 *	- or -
	 * 		http://example.com/index.php/welcome/index
	 *	- or -
	 * Since this controller is set as the default controller in
	 * config/routes.php, it's displayed at http://example.com/
	 *
	 * So any other public methods not prefixed with an underscore will
	 * map to /index.php/welcome/<method_name>
	 * @see https://codeigniter.com/user_guide/general/urls.html
	 */
	
	public function __construct() {

            parent::__construct();

            $this->load->database();


        }
	public function Bookings()
	{
		$AllBookings = $this->db->where('USR_REF',$memberid)->get('member_entry_info')->result();
		if(!$checkRegistration)
		{
			$data["ReserveRoomResult"]=["success"=> false, "message" => "Fail"];
			echo json_encode($data);
			exit;
		}
	}
	//Check membership
	public function CheckMembership()
	{
		$id=$_POST['memberid'];
		$query = $this->db->select('USR_SN,USR_NAME ,USR_PASSWORD')->where('USR_NAME',$id)->get('sec_users')->result();
		
		if($query)
		{
			http_response_code(200);
			$res=$this->db->select('USR_REF,MEMBER_NAME,AUTO_SMS_NO as MOBILE_NO ,STATUS')->where('USR_REF',$query[0]->USR_SN)->get('member_entry_info')->result();
			$data["CheckMembershipResult"]=["success"=> true, "message" => "Success" , "data" => $res];
			echo json_encode($data);
		}
		else
		{
			http_response_code(200);
			$data["CheckMembershipResult"]=["success"=> false, "message" => "Failed! Invalid member Id" , "data" => array()];
			echo json_encode($data);
			}
			exit;
	}
	public function CheckAvailablity()
	{
		$memberid=$_POST['memberid'];
		$date_from=$_POST['date_from'];
		$date_to=$_POST['date_to'];
		$no_of_days=$_POST['no_of_rooms'];
		//get member Details
		
			http_response_code(200);
			$member=$this->db->where('USR_REF',$memberid)->get('member_entry_info')->result();
			
		$query = $this->db->select('RCAT_SN,RCAT_TITLE,RCAT_FULLCHARGE,RCAT_GUEST_FULLCHARGE')->get('cnf_roomscategory')->result_array();
		
		if(count($query))
		{
			
			foreach ($query as $key=>$value) {
				$images = $this->db->select('IMAGES_URL')->where("RCNF_ID",$value["RCAT_SN"])->get('cnf_roomscategory_images')->result_array();
				$query[$key]["images"]=$images;
				$query[$key]["no_of_rooms_available"]="2";
				
			}
				
			 http_response_code(200);
			$data["CheckAvailablityResult"]=["success"=> true, "message" => "Success" , "data" =>$query];
            echo json_encode($data);
			
		}
		else
		{
			http_response_code(200);
		$data["CheckAvailablityResult"]=["success"=> false, "message" => "Failed!" , "data" => array()];
            echo json_encode($data);
			
		}
	}
	
	public function RulesAndRegulations()
	{
		 $query = $this->db->get('app_rulesandregulations')->result_array();
		
		
			 http_response_code(200);
			//$data["GetDetailsResult"]=["success"=> true, "message" => "Success" , "data" =>$query[0]];
            echo $query[0]["RulesAndRegulations"];
			
	
	}
	
	
	
	public function ReserveRoom()
	{ 
		$memberid=$_POST['memberid'];
		$no_of_guest=$_POST['no_of_guest'];
		$memberno=$_POST['member_number'];
		$date_from=$_POST['date_from'];
		$date_to=$_POST['date_to'];
		$no_of_rooms=$_POST['no_of_rooms'];
		$RCAT_SN=$_POST['RCAT_SN'];
		$type=$_POST['Type'];
		
	
		$checkRegistration = $this->db->where('USR_REF',$memberid)->get('member_entry_info')->result();
		if(!$checkRegistration)
		{
			$data["ReserveRoomResult"]=["success"=> false, "message" => "Fail"];
			echo json_encode($data);
			exit;
		}
		$query = $this->db->where('REG_CLUBCARDNO',$memberno)->get('trn_registration')->result();
		$rsvcode="1";
		$rsvcodeResult = $this->db->select('RSV_CODE')->order_by("RSV_SN","desc")->get('trn_reservations')->result();
		if($rsvcodeResult)
		{
			$rsvcode=($rsvcodeResult[0]->RSV_CODE+1)."";
		}
		
		if(!$query)
		{
			//registration ofthe user
			$REG_CODE='ICM-221287';
			$REG_CODEResult = $this->db->select('REG_CODE')->order_by("REG_SN","desc")->get('trn_registration')->result();
			if($REG_CODEResult)
			{
				$iparr = explode("-", $REG_CODEResult[0]->REG_CODE); 
				$REG_CODE=($iparr0[1]+1)."";
			}
			$dataprofile = array(
			 'REG_MCAT_REF' => '1',
			 'REG_CODE' => $REG_CODE,
			 'REG_DATE' => date('Y-m-d H:i:s'),
			 'REG_NAME' => $checkRegistration[0]->MEMBER_NAME,
			 'REG_CLUB_REF' => '1',
			 'REG_CLUBCARDNO' => $memberno,
			 'REG_RESPHONE' => $checkRegistration[0]->RES_PHONE_NO,
			 'REG_NICPLACEOFISSUE' => '',
			 'REG_OFFPHONE' => $checkRegistration[0]->OFF_PHONENO,
			 'REG_ADDRESS' => $checkRegistration[0]->MAILING_ADDRESS,
			 'REG_MOBPHONE' => $checkRegistration[0]->MOBILE_NO,
			 'REG_PASSPORT' => '',
			 'REG_NIC' => $checkRegistration[0]->NIC_NO,
			 'REG_NATIONALITY' => '',
			 'REG_PROFESSION' => ''
			 );
			$this->db->insert('trn_registration',$dataprofile);
			$query = $this->db->where('REG_CLUBCARDNO',$memberno)->get('trn_registration')->result();
		}
		
		//Reservation table
		$datareservation = array(
			 'RSV_CODE' => $rsvcode,
			 'RSV_RSVT_REF' => '2',
			 'RSV_GS_REF' => '1',
			 'RSV_EXPECTEDARRIVAL' => $date_from." 04:00:00",
			 'RSV_REG_REF' => $query[0]->REG_SN,
			 'RSV_ACTUALARRIVAL' => $date_from." 04:00:00",
			 'RSV_RESERVATIONDATE' => date('Y-m-d H:i:s'),
			 'RSV_EXPECTEDDEPARTURE' => $date_to." 03:00:00",
			 'RSV_ACTUALDEPARTURE' => $date_to." 03:00:00",
			 'RSV_REMARKS' => '',
			 'RSV_STATUS' => 'A',
			 'RSV_NOOFROOMS' => $no_of_rooms,
			 'RSV_TOTALGUESTS' => $no_of_guest,
			 'RSV_CBY' => $memberid,
			 'RSV_CDATE' => date('Y-m-d H:i:s'),
			'RSV_CONFBY' => '0',
			 'RSV_CONFDATE' => date('Y-m-d H:i:s'),
			 'RSV_CONFFROM' => '',
			 'RSV_CONFPRINTBY' => '0',
			 'RSV_CONFPRINTDATE' =>date('Y-m-d H:i:s'),
			 'RSV_DLVL_REF' => '1',
			 'RSV_PL_STATUS' => ''
			 );
			$this->db->insert('trn_reservations',$datareservation);
			
			$insert_id = $this->db->insert_id();

		//Reservation Details
			$DetailsId='1';
			$DetailsIdResult = $this->db->select('RSVD_SN')->order_by("RSVD_SN","desc")->get('trn_reservations_det')->result();
			if($DetailsIdResult)
			{
				$DetailsId=($DetailsIdResult[0]->RSVD_SN+1)."";
			}
			$datareservation_Det = array(
			 'RSVD_SN' =>$DetailsId,
			 'RSVD_RSV_REF' => $insert_id,
			 'RSVD_ROOMNO' => $no_of_rooms,
			 'RSVD_STATUS' => 'A',
			 'RSVD_CINBY' => $memberid,
			 'RSVD_NOOFPERSONS' => $no_of_guest,
			 'RSVD_GST_REF' => ''
			 );
			$this->db->insert('trn_reservations_det',$datareservation_Det);
			
		//Reservation Allocation
			$AllocationId='1';
			$AllocationIdResult = $this->db->select('AS_RSVD_REF')->order_by("AS_RSVD_REF","desc")->get('trn_allocschedule')->result();
			if($AllocationIdResult)
			{
				$AllocationId=($AllocationIdResult[0]->RSVD_SN+1)."";
			}
			$datareservation_Allocation = array(
			 'AS_RSVD_REF' =>$AllocationId,
			 'AS_R_REF' => $DetailsId,
			 'AS_FROMDATE' => $date_from,
			 'AS_TODATE' => $date_to,
			 'AS_STATUS' => 'A',
			 'AS_EXPFROMDATE' =>$date_from,
			 'AS_EXPTODATE' => $date_to,
				'AS_CINBY' => $memberid,
			 'AS_CINDATE' => $date_from,
			 'AS_COUTDATE' => $date_to,
			 'AS_USR_REF' => $memberid,
			 'AS_SYSDATE' => date('Y-m-d H:i:s')
			 );
			$this->db->insert('trn_allocschedule',$datareservation_Allocation);
			
		$data["ReserveRoomResult"]=["success"=> true, "message" => "Success"];
		echo json_encode($data);
	}
	
	public function GetDetails()
	{
		$query = $this->db->get('app_category')->result_array();
		
		if($query)
		{
			foreach ($query as $key=>$value) {
				
				$images = $this->db->where("CategoryId",$value["Id"])->get('app_category_images')->result_array();
					
				$query[$key]["images"]=$images;
				
			}
		
		
			
			foreach ($query as $key=>$value) {
				$subcat = $this->db->where("CategoryId",$value["Id"])->get('app_subcategory')->result_array();
				
				foreach ($subcat as $key1=>$value1) {
				$images_sub = $this->db->where("SubCategoryId",$value1["Id"])->get('app_subcategory_images')->result_array();
					if(count($images_sub)>0)
					{
						
						$subcat[$key1]["Sub_Cat_Images"]=$images_sub;
						$query[$key]["SubCategory"]=$subcat;
					}
					else
					{
						$subcat[$key1]["Sub_Cat_Images"]=array();
						$query[$key]["SubCategory"]=$subcat;
					}
				
			}
				
				
			}
			
			http_response_code(200);
			$data["GetDetailsResult"]=["success"=> true, "message" => "Success" , "data" => $query];
			echo json_encode($data);
		}
		else
		{
			http_response_code(200);
			$data["CheckMembershipResult"]=["success"=> false, "message" => "Failed! Invalid member Id" , "data" => array()];
			echo json_encode($data);
			}
			exit;
	}
	
}
