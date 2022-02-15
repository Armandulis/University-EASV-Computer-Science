import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Socket} from 'ngx-socket-io';
import {Observable, of} from 'rxjs';
import {environment} from '../../environments/environment';
import {map} from 'rxjs/operators';
import {Settings} from './settings';
import {Measurement} from './measurement';

@Injectable({
  providedIn: 'root'
})
/**
 * Class SettingsService
 */
export class SettingsService {

  /**
   * SettingsService constructor
   */
  constructor(private http: HttpClient,
              private socket: Socket) {
  }

  /**
   * Change current Settings
   */
  changeSetting( maxValue: number | null, minValue: number | null): Observable<Settings> {

    // If there is no value, return observable null
    if ( minValue == null || maxValue == null) {
      return of( null );
    }

    console.log(environment.url + '/settings');
    console.log(maxValue + ' ' + minValue);
    // If there was a value, return Observable Measurement
    return this.http.put<Settings>(environment.url + '/settings',  {
      sensorId: environment.settingsSensorId,
      maxTemperature: maxValue,
      minTemperature: minValue,
      insertTime: Date.now()
    });
  }

  /**
   * Get current Settings
   */
  getCurrentSettings(): Observable<Settings> {
    return this.http.get<Settings>(environment.url + '/settings/latest' );
  }

  listenForSettingsSeries(sensorId: string): Observable<Settings> {
    console.log(sensorId);
    console.log('hello');
    return this.socket.fromEvent<Settings>(sensorId)
      .pipe(map(settings => {
        console.log('armandas');
        return {
          sensorId: settings.sensorId,
          maxTemperature: settings.maxTemperature,
          minTemperature: settings.minTemperature,
          insertTime: settings.insertTime
        };
      }));
  }
}
