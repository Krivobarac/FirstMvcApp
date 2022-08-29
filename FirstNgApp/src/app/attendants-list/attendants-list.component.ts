import { Component, OnInit } from '@angular/core';
import { AttendantService } from '../_services/attendant.service';

@Component({
  selector: 'app-attendants-list',
  templateUrl: './attendants-list.component.html',
  styleUrls: ['./attendants-list.component.scss']
})
export class AttendantsListComponent implements OnInit {

  constructor(public service: AttendantService) { }
 
  ngOnInit(): void {
    this.getAttendants();
  }
 
  private getAttendants() {
    this.service.getAttendants().subscribe(
      list => {
        this.service.people = list;
      });
  }
}
