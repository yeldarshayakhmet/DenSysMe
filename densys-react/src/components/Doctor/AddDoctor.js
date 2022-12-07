import React from 'react';
import {Link} from 'react-router-dom';
import {getToken, useFormInput} from "../../Utils/Common";
import axios from "axios";
import {
	Row,
	Col,
	Nav,
	NavItem,
	NavLink,
	Form,
	FormGroup,
	Label,
	Input,
	Button,
} from "reactstrap";


function AddDoctor() {
  const dateOfBirth = useFormInput('');
  const iin = useFormInput('');
  const firstName = useFormInput('');
  const lastName = useFormInput('');
  const phone = useFormInput('');
  const yearsOfExperience = useFormInput('');
  const appointmentPrice = useFormInput('');
  const address = useFormInput('');
  
  const handleCreateDoctor = () => {
    axios.post("http://localhost:5001/api/employees/doctors", {
      request: {
        dateOfBirth: dateOfBirth.value,
        iin: iin.value,
        firstName: firstName.value,
        lastName: lastName.value,
        phone: phone.value,
        yearsOfExperience: yearsOfExperience.value,
        appointmentPrice: appointmentPrice.value,
        address: address.value
      }
    }, {
      headers: {
        'Authorization': 'Bearer ' + getToken(),
        'Content-Type': 'multipart/form-data'
      }
    })
  }
  
  return (
    <div>
    <Nav tabs>
					<NavItem>
						<NavLink active>
							<Link to="/addDoctor">Add Doctor</Link>
						</NavLink>
					</NavItem>
					<NavItem>
						<NavLink>
							<Link to="/doctor">Doctor List</Link>
						</NavLink>
					</NavItem>
				</Nav>
   <Form >
    <FormGroup>
      <Label>Date of Birth</Label>
      <Input type="date" {...dateOfBirth} placeholder='Date of birth'></Input>
      <Label>IIN</Label>
      <Input type="text" {...iin} placeholder='IIN'></Input>
      <Label>Name</Label>
      <Input type="text" {...firstName} placeholder='Name'></Input>
      <Label>Lastname</Label>
      <Input type="text" {...lastName} placeholder='Surname'></Input>
      <Label>Contact number</Label>
      <Input type="text" {...phone} placeholder='without +'></Input>
      <Label>Experience in years</Label>
      <Input type="number" {...yearsOfExperience} placeholder='just number '></Input>
      <Label>Price of appointment</Label>
      <Input type="number" {...appointmentPrice} placeholder='$'></Input>
      <Label>Address</Label>
      <Input type="text" {...address} placeholder='Address'></Input>
    </FormGroup>
    <Button type="submit" onClick={alert(666)}> Add Doctor </Button>
   </Form>
   </div>
  );
}

export default AddDoctor;

