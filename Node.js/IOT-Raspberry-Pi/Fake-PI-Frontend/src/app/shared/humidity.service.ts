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
 * Class HumidityService
 */
export class HumidityService {

  /**
   * HumidityService constructor
   */
  constructor(private http: HttpClient,
              private socket: Socket) {
  }

  /**
   * Change current Humidity
   */
  changeHumidity(value: number | null): Observable<Measurement> {

    console.log(value);
    // If there is no value, return observable null
    if (value == null) {
      return of(null);
    }

    console.log(environment.url + '/Humidity');
    // If there was a value, return Observable Measurement
    return this.http.put<Measurement>(environment.url + '/humidity', {
      sensorId: environment.humiditySensorId,
      value: value,
      measurementTime: Date.now()
    });
  }

  /**
   * Get current Humidity
   */
  getCurrentHumidity(): Observable<Measurement> {
    return this.http.get<Measurement>(environment.url + '/humidity/latest');
  }

  listenForDataSeries(): Observable<DataEntry> {
    console.log('hello');
    this.socket.on('connect_error', (err) => {
      console.log('woopsies');
      console.log(err);
    });
    return this.socket.fromEvent<Measurement>(environment.humiditySensorId)
      .pipe(map(measurement => {
        console.log('armandas');
        return {
          name: measurement.measurementTime.toString(),
          value: measurement.value
        };
      }));
  }

  getDataSeries(): Observable<DataSeries> {
    return this.http.get<Measurement[]>(environment.url + '/humidity?sensorId=' + environment.humiditySensorId)
      .pipe(map(measurements => {
        const series: DataEntry[] = [];
        const dataSeries: DataSeries = {
          name: environment.humiditySensorId,
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
