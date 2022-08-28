import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AddAttendantComponent } from './add-attendant/add-attendant.component';
import { AttendantsListComponent } from './attendants-list/attendants-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AddAttendantComponent,
    AttendantsListComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
