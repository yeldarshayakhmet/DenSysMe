
import React from 'react';
import {Form, FormGroup, Label, Input, Button} from 'reactstrap';
import {Link} from 'react-router-dom';


function AddDoctor() {
  return (
   <Form>
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
      <Label>Contact numbet</Label>
      <Input type="number" placeholder='without +'></Input>
      <Label>Department Id</Label>
      <Input type="text" placeholder=' '></Input>
      <Label>Specialization Id</Label>
      <Input type="text" placeholder=' '></Input>
      <Label>Experience in years</Label>
      <Input type="number" placeholder='just number '></Input>
      <Label>Photo</Label>
      <Input type="image" ></Input>
      <Label>Category</Label>
      <Input type="text" placeholder='Highest, First, ...'></Input>
      <Label>Price of appointment</Label>
      <Input type="number" placeholder='$'></Input>
      <Label>Schedule details</Label>
      <Input type="text" placeholder='Address'></Input>
      <Label>Degree/education</Label>
      <Input type="text" placeholder='MD, PhD, etc'></Input>
      <Label>Rating</Label>
      <Input type="number" placeholder='from 0 to 10'></Input>
      <Label>Address</Label>
      <Input type="text" placeholder='Address'></Input>
      <Label>Homepage URL</Label>
      <Input type="url" placeholder='any details about patient'></Input>
    </FormGroup>
    <Button type="submit"> Submit</Button>
    <Link to ="/" className='btn btn-danger'>Cancel</Link>
   </Form>
  );
}

export default AddDoctor;

