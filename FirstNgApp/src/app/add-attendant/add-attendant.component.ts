import { Component, OnInit } from '@angular/core';
import { Person } from '../_models/person';
import { AttendantService } from '../_services/attendant.service';

@Component({
  selector: 'app-add-attendant',
  templateUrl: './add-attendant.component.html',
  styleUrls: ['./add-attendant.component.scss']
})
export class AddAttendantComponent {

  constructor(public service: AttendantService) { }

  public onAttendantSave() {
    this.service.addAttendant().subscribe(data => {
      this.service.person.id = data.id;
      this.service.people.push(this.service.person);
 
      this.service.person = new Person();
      this.service.personFormModel.reset();
    });
  }
}
