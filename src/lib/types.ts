export interface RunEvent {
  date: string
  location: string
  status: string
  notes: string | null
}

export interface SocialLink {
  name: string
  url: string
}

export interface Location {
  name: string
  address: string
  city: string
  state: string
  zip: string
  latitude: number
  longitude: number
}

export interface OtherEvent {
  date: string
  location: string
  notes: string | null
}

export interface SiteConfig {
  showEvents: boolean
  showMap: boolean
}
