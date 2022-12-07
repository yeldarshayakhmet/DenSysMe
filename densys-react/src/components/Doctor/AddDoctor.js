import React from 'react';
import {api, getToken} from "../../Utils/Common";

class AddDoctor extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            iin: '',
            firstName: '',
            middleName: '',
            lastName: '',
            dateOfBirth: '',
            phone: '',
            email: '',
            yearsOfExperience: 0,
            appointmentPrice: 0.0,
            address: '',
            category: 1,
            degree: 1,
            specializationId: 0,
            specializations: []
        };
        this.createDoctor = this.createDoctor.bind(this);
        api.get("specializations", {
            headers: {
                'Authorization': 'Bearer ' + getToken()
            }
        }).then(res => {
            console.log(res.data);
            this.setState({ specializations: res.data, specializationId: res.data[0].id });
        });
    }
    
    createDoctor(event) {
        event.preventDefault();
        let form = new FormData();
        form.append('request', JSON.stringify(this.state));
        form.append('photo', document.getElementById("photo").files[0])
        api.post('employees/doctors', form, {
            headers: {
                'Authorization': 'Bearer ' + getToken(),
                'Content-Type': 'multipart/form-data'
            }
        }).then(res => console.log(res)).catch(err => console.log(err));
    }

    /*function AddDoctor() {
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
    }*/
    render() {
        return (
            <form onSubmit={this.createDoctor}>
                <div>
                    <label>IIN</label>
                    <input
                        type="text"
                        value={this.state.iin}
                        onChange={event => {
                            this.setState({iin: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>FirstName</label>
                    <input
                        type="text"
                        value={this.state.firstName}
                        onChange={event => {
                            this.setState({firstName: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>LastName</label>
                    <input
                        type="text"
                        value={this.state.lastName}
                        onChange={event => {
                            this.setState({lastName: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>MiddleName</label>
                    <input
                        type="text"
                        value={this.state.middleName}
                        onChange={event => {
                            this.setState({middleName: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>Date Of Birth</label>
                    <input
                        type="date"
                        value={this.state.dateOfBirth}
                        onChange={event => {
                            this.setState({dateOfBirth: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>Phone</label>
                    <input
                        type="text"
                        value={this.state.phone}
                        onChange={event => {
                            this.setState({phone: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>Email</label>
                    <input
                        type="text"
                        value={this.state.email}
                        onChange={event => {
                            this.setState({email: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>Years Of Experience</label>
                    <input
                        type="number"
                        value={this.state.yearsOfExperience}
                        onChange={event => {
                            this.setState({yearsOfExperience: parseInt(event.target.value)})
                        }}
                    />
                </div>
                <div>
                    <label>Appointment Price</label>
                    <input
                        type="number"
                        value={this.state.appointmentPrice}
                        onChange={event => {
                            this.setState({appointmentPrice: parseFloat(event.target.value)})
                        }}
                    />
                </div>
                <div>
                    <label>Address</label>
                    <input
                        type="text"
                        value={this.state.address}
                        onChange={event => {
                            this.setState({address: event.target.value})
                        }}
                    />
                </div>
                <div>
                    <label>Degree</label>
                    <select value={this.state.degree} onChange={event => { this.setState({degree: parseInt(event.target.value)}) }}>
                        <option value = {1}>MD</option>
                        <option value = {2}>PhD</option>
                        <option value = {3}>Master</option>
                        <option value = {4}>Bachelor</option>
                    </select>
                </div>
                <div>
                    <label>Category</label>
                    <select value={this.state.category} onChange={event => { this.setState({category: parseInt(event.target.value)}) }}>
                        <option value = {1}>First</option>
                        <option value = {2}>Second</option>
                        <option value = {3}>Highest</option>
                        <option value = {4}>None</option>
                    </select>
                </div>
                <div>
                    <label>Specialization</label>
                    <select value = {this.state.specializationId} onChange={event => { this.setState({ specializationId: event.target.value }) }}> {
                            this.state.specializations.map(spec => <option key={spec.id} value = {spec.id}>{spec.name}</option>)
                        }
                    </select>
                </div>
                <div>
                    <label>Photo</label>
                    <input
                        type="file"
                        id="photo"
                        accept="image/jpeg"
                    />
                </div>
                <button type="submit">Create</button>
            </form>
        );
        /*return(
        <Form>
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
        <Button type="submit" onClick={handleCreateDoctor}> Add Doctor </Button>
        <Link to="/" className='btn btn-danger'>Cancel</Link>
        </Form>);
    }*/
    }
}

export default AddDoctor;