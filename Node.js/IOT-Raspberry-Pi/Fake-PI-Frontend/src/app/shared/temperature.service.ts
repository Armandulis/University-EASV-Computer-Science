import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Socket} from 'ngx-socket-io';
import {Observable, of} from 'rxjs';
import {Measurement} from './measurement';
import {environment} from '../../environments/environment';
import {map} from 'rxjs/operators';
import {DataEntry} from './data-entry';
import {DataSeries} from './data-series';

@Injectable({
  providedIn: 'root'
})
/**
 * Class TemperatureService
 */
export class TemperatureService {

  /**
   * TemperatureService constructor
   */
  constructor(private http: HttpClient,
              private socket: Socket) {
  }

  /**
   * Change current temperature
   */
  changeTemperature(value: number | null): Observable<Measurement> {

    console.log(value);
    // If there is no value, return observable null
    if (value == null) {
      return of(null);
    }

    console.log(environment.url + '/temperature');
    // If there was a value, return Observable Measurement
    return this.http.put<Measurement>(environment.url + '/temperature', {
      sensorId: environment.temperatureSensorId,
      value: value,
      measurementTime: Date.now()
    });
  }

  /**
   * Get current temperature
   */
  getCurrentTemperature(): Observable<Measurement> {
    return this.http.get<Measurement>(environment.url + '/temperature/latest');
  }

  listenForDataSeries(): Observable<DataEntry> {
    console.log('hello');
    this.socket.on('connect_error', (err) => {
      console.log('something went wrong');
      console.log(err);
    });
    return this.socket.fromEvent<Measurement>(environment.temperatureSensorId)
      .pipe(map(measurement => {
        console.log('armandas');
        return {
          name: measurement.measurementTime.toString(),
          value: measurement.value
        };
      }));
  }

  getDataSeries(): Observable<DataSeries> {
    return this.http.get<Measurement[]>(environment.url + '/temperature?sensorId=' + environment.temperatureSensorId)
      .pipe(map(measurements => {
        const series: DataEntry[] = [];
        const dataSeries: DataSeries = {
          name: environment.temperatureSensorId,
          series: series,
        };
        if (measurements instanceof Array) {
          measurements.forEach(m => {
            series.push({
              name: m.measurementTime.toString(),
              value: m.value
            });
          });
        }
        return dataSeries;
      }));
  }
}
