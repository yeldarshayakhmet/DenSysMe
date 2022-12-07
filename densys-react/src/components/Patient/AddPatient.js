
import React from 'react';
import {Form, FormGroup, Label, Input, Button, Row, Col, Nav, NavItem, NavLink } from 'reactstrap';
import {Link} from 'react-router-dom';


function AddPatient() {
  return (
    <div>
      <Nav tabs>
					<NavItem>
						<NavLink active>
							<Link to="/addPatient">Add Patient</Link>
						</NavLink>
					</NavItem>
					<NavItem>
						<NavLink>
							<Link to="/patientlist">
								Patient List
							</Link>
						</NavLink>
					</NavItem>
				</Nav>

   <Form >
    <FormGroup>
      <Label>Date of Birth</Label>
      <Input type="date" placeholder='Date of birth'></Input>
      <Label>INN</Label>
      <Input type="text" placeholder='INN'></Input>
      <Label>ID number</Label>
      <Input type="text" placeholder='IDnumber'></Input>
      <Label>Name</Label>
      <Input type="text" placeholder='Name'></Input>
      <Label>Surname</Label>
      <Input type="text" placeholder='Surname'></Input>
      <Label>Blood group</Label>
      <Input type="text" placeholder='Blood type: A, B , AB, O '></Input>
      <Label>Emergency contact number </Label>
      <Input type="number" placeholder='without +'></Input>
      <Label>Contact numbet</Label>
      <Input type="number" placeholder='without +'></Input>
      <Label>Email</Label>
      <Input type="email" placeholder='email'></Input>
      <Label>Address</Label>
      <Input type="text" placeholder='Address'></Input>
      <Label>Martial Status</Label>
      <Input type="text" placeholder=' married, divorced,  widowed, single'></Input>
      <Label>Registration Date</Label>
      <Input type="date" placeholder='date'></Input>
      <Label>Details</Label>
      <Input type="text" placeholder='any details about patient'></Input>
    </FormGroup>
    <Button type="submit"> Submit</Button>
   </Form>
   </div>
  );
}

export default AddPatient;

