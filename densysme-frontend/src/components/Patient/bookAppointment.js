import React from "react";
import { Link } from "react-router-dom";
import {
	Button,
	Form,
	FormGroup,
	Input,
	Label,
	NavItem,
	NavLink,
	Nav,
	Row,
	Col,
} from "reactstrap";

import { api, getToken } from "../../Utils/Common";

class BookAppointment extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			time: '',
			firstName: '',
			lastName: '',
			iin: '',
			phone: ''
		};
		this.handleSubmit = this.handleSubmit.bind(this)
	}
	
	handleSubmit() {
		const data = {
			doctorId: this.props.location.doctorId,
			time: this.state.time,
			patient: {
				firstName: this.state.firstName,
				lastName: this.state.lastName,
				iin: this.state.iin,
				phone: this.state.phone
			}
		}
		const headers = {
			Authorization: 'Bearer ' + getToken(),
		};
		api.post("appointments", data, {
				headers: headers,
			}).then((res) => {
				console.log(res);
			});
	}

	render() {
		return (
			<div>
				<Nav tabs>
					<NavItem>
						<NavLink>
							<Link to="/doctors">Doctor List</Link>
						</NavLink>
					</NavItem>
				</Nav>
				<Row>
					<Col md="3"></Col>
					<Col md="6">
						<Form className="mt-3">
							<FormGroup>
								<Label>Time *</Label>
								<Input
									type="datetime-local"
									value={this.state.time}
									onChange={event => {
										this.setState({ time: event.target.value });
									}}
								/>
							</FormGroup>
							<FormGroup>
								<Label>First Name *</Label>
								<Input
									type="text"
									value={this.state.firstName}
									onChange={event => {
										this.setState({ firstName: event.target.value });
									}}
								/>
							</FormGroup>
							<FormGroup>
								<Label>Last Name *</Label>
								<Input
									type="text"
									value={this.state.lastName}
									onChange={event => {
										this.setState({ lastName: event.target.value });
									}}
								/>
							</FormGroup>
							<FormGroup>
								<Label>IIN *</Label>
								<Input
									type="text"
									value={this.state.iin}
									onChange={event => {
										this.setState({ iin: event.target.value });
									}}
								/>
							</FormGroup>
							<FormGroup>
								<Label>Phone *</Label>
								<Input
									type="text"
									value={this.state.phone}
									onChange={event => {
										this.setState({ phone: event.target.value });
									}}
								/>
							</FormGroup>

							<FormGroup>
								<Button color="primary" onClick={this.handleSubmit}>Book</Button>
							</FormGroup>
						</Form>
					</Col>
				</Row>
			</div>
		);
	}
}
export default BookAppointment;
