
import React from 'react';
import {Form, FormGroup, Label, Input, Button} from 'reactstrap';
import {Link} from 'react-router-dom';

export function EditPatient() {
  return (
    <Form>
    <FormGroup>
      <Label>Edit Date of Birth</Label>
      <Input type="date" placeholder='Date of birth'></Input>
      <Label>Edit INN</Label>
      <Input type="text" placeholder='INN'></Input>
      <Label>Edit ID number</Label>
      <Input type="text" placeholder='IDnumber'></Input>
      <Label>Edit Name</Label>
      <Input type="text" placeholder='Name'></Input>
      <Label>Edit Surname</Label>
      <Input type="text" placeholder='Surname'></Input>
      <Label>EditBlood group</Label>
      <Input type="text" placeholder='Blood type: A, B , AB, O '></Input>
      <Label>Edit Emergency contact number </Label>
      <Input type="number" placeholder='without +'></Input>
      <Label>Edit Contact numbet</Label>
      <Input type="number" placeholder='without +'></Input>
      <Label>Edit Email</Label>
      <Input type="email" placeholder='email'></Input>
      <Label>Edit Address</Label>
      <Input type="text" placeholder='Address'></Input>
      <Label>Edit Martial Status</Label>
      <Input type="text" placeholder=' married, divorced,  widowed, single'></Input>
      <Label>Edit Registration Date</Label>
      <Input type="date" placeholder='date'></Input>
      <Label>Edit Details</Label>
      <Input type="text" placeholder='any details about patient'></Input>
    </FormGroup>
    <Button type="submit"> Submit</Button>
    <Link to ="/" className='btn btn-danger'>Cancel</Link>
   </Form>
  );
}

export default EditPatient;
