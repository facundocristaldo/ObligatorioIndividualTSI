import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { EmployeeModel } from '../EmployeeModel';
/**
 * Service for notify and subscribe to events.
 */
@Injectable()
export class EmployeeService {
   
    token: string = "";

    constructor(private http: Http) {
        console.log('employee service constructor');

        this.creartoken().subscribe((res: Response) => { this.token = res.json(); console.log( this.token); });

    }

    public addEmployee(Name: string, Type: number, StartDate: Date | undefined, value: number): Observable<any> {

        console.log('add employee');


        const head = new Headers();
        head.set("Content-Type", "application/json");
        head.set("Authorization", "Bearer " + this.token);
        const options = new RequestOptions({ headers: head });

        var a = new EmployeeModel(0, Name, Type, StartDate, value);

        return this.http.post('http://localhost:51364/api/Employee/?type=' + Type, {
            Id: a.Id,
            Name: a.Name,
            Type: a.Type,
            Salary: a.Salary,
            HourlyRate: a.HourlyRate,
            StartDate: a.StartDate
        },options);

    }

    public editEmployee() {
        console.log('edit employee');
    }

    public getEmployees(idEmp: number, nombreEmp: string, tipoEmp: string, fechaEmp: Date | undefined): Observable<any> {
        console.log("fecha empleado vacio = ", fechaEmp);

        if (idEmp ===null) {

            idEmp = 0;
        }
        if (nombreEmp == "") {
            nombreEmp = "nosearch";
        }

        const head = new Headers();
        head.set("Content-Type", "application/json");
        head.set("Authorization", "Bearer " + this.token);
        const options = new RequestOptions(
            {
                headers: head,
                params:
                    {
                        idemp: idEmp,
                        nombreEmp: nombreEmp,
                        tipoEmp: tipoEmp,
                        fechaEmp: fechaEmp
                    }
            }
        );
        return this.http.get("http://localhost:51364/api/Employee", options);
        
    }

    public deleteEmployee(id: number) {
        console.log("Delete employee number " + id);
        const head = new Headers();
        head.set("Content-Type", "application/json");
        head.set("Authorization", "Bearer " + this.token);
        const options = new RequestOptions({ headers: head });
        this.http.delete("http://localhost:51364/api/Employee/" + id, options).subscribe();

    }

    creartoken(): Observable<any>{
        const head = new Headers();
        

        const options = new RequestOptions({ headers: head });

        return this.http.get("http://localhost:51364/api/Token/CreateToken?name=defaultauth");
    }
}
